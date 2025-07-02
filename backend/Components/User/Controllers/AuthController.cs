using backend.Components.Student.Services.Interfaces;
namespace backend.User.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.User.DTOs;
using backend.User.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJWTService _jwtService;
    private readonly IUserService _userService;
    private readonly IStudentProfileService _studentService;

    public AuthController(
        IAuthService authService,
        IJWTService jwtService,
        IUserService userService,
        IStudentProfileService studentService)
    {
        _authService = authService;
        _jwtService = jwtService;
        _userService = userService;
        _studentService = studentService;
    }

    // POST /api/auth/signup/applicant
    // separate endpoint prevents usertype manipulation
    [HttpPost("signup/applicant")]
    public async Task<ActionResult<AuthResponseDTO>> SignupApplicant([FromBody] ApplicantSignupDTO signupDto)
    {
        try
        {
            // validate model state from data annotations
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.SignupApplicantAsync(signupDto);
            
            // Set refresh token in httpOnly cookie
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenCookie(result.RefreshToken);
            }
            
            // Don't send refresh token in response body
            result.RefreshToken = null;
            
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            // business rule validation errors (e.g., invalid email format)
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            // business logic errors (e.g., email already exists)
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // unexpected errors - don't expose internal details
            return StatusCode(500, new { message = "An error occurred during registration" });
        }
    }

    // POST /api/auth/signup/recruiter
    // separate endpoint prevents usertype manipulation
    [HttpPost("signup/recruiter")]
    public async Task<ActionResult<AuthResponseDTO>> SignupRecruiter([FromBody] RecruiterSignupDTO signupDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.SignupRecruiterAsync(signupDto);
            
            // Set refresh token in httpOnly cookie
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenCookie(result.RefreshToken);
            }
            
            // Don't send refresh token in response body
            result.RefreshToken = null;
            
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during registration" });
        }
    }

    // POST /api/auth/login
    // unified login for all user types
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginDTO loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(loginDto);
            
            // Set refresh token in httpOnly cookie
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenCookie(result.RefreshToken);
            }
            
            // Don't send refresh token in response body
            result.RefreshToken = null;
            
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            // credential validation errors
            return Unauthorized(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during login" });
        }
    }

    // POST /api/auth/refresh
    // refreshes access token using refresh token from cookie
    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponseDTO>> RefreshToken()
    {
        try
        {
            // Get refresh token from httpOnly cookie
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Unauthorized(new { message = "Refresh token not found" });
            }

            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            }

            // Set new refresh token in httpOnly cookie
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenCookie(result.RefreshToken);
            }

            // Return new access token (don't include refresh token in response body)
            return Ok(new AuthResponseDTO
            {
                Token = result.Token,
                ExpiresAt = result.ExpiresAt,
                User = result.User,
                RefreshToken = null // Don't send refresh token in response body
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during token refresh" });
        }
    }

    // POST /api/auth/logout
    // revokes refresh token and clears cookies
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        try
        {
            // Get refresh token from cookie
            var refreshToken = Request.Cookies["refreshToken"];

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                // Revoke the refresh token in database
                await _authService.RevokeRefreshTokenAsync(refreshToken);
            }

            // Clear the refresh token cookie
            Response.Cookies.Delete("refreshToken");

            return Ok(new { message = "Logged out successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during logout" });
        }
    }

    // POST /api/auth/check
    // validates if the provided email belongs to a valid student profile
    [HttpPost("check")]
    public async Task<ActionResult<CheckStudentResponseDTO>> CheckStudent([FromBody] CheckStudentRequestDTO request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exists = await _studentService.StudentExistsAsync(request.Email);
        if (!exists)
            return NotFound(new { message = "No Student Found" });

        return Ok(new CheckStudentResponseDTO { IsValid = true });
    }

    // POST /api/auth/validate
    // validates if token is still valid
    [HttpPost("validate")]
    public async Task<ActionResult> ValidateToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return BadRequest(new { message = "Token is required" });
            }

            var isValid = await _authService.ValidateTokenAsync(request.Token);
        
            if (!isValid)
            {
                return Ok(new { isValid = false });
            }

            // Get user info from token
            var userId = _jwtService.GetUserIdFromToken(request.Token);
            if (userId == null)
            {
                return Ok(new { isValid = false });
            }

            var user = await _userService.GetUserByIdAsync(userId.Value);
            if (user == null || !user.GetIsActive())
            {
                return Ok(new { isValid = false });
            }
        
            return Ok(new { 
                isValid = true, 
                userType = user.GetUserType(),
                userId = user.GetUserId(),
                email = user.GetEmail(),
                firstName = user.GetFirstName(),
                lastName = user.GetLastName(),
                isActive = user.GetIsActive(),
                isVerified = user.GetIsVerified()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during token validation" });
        }
    }

    // POST /api/auth/verify/{userId}
    // verifies user account (for email verification)
    [HttpPost("verify/{userId}")]
    public async Task<ActionResult> VerifyUser(int userId)
    {
        try
        {
            if (userId <= 0)
            {
                return BadRequest(new { message = "Invalid user ID" });
            }

            var success = await _authService.VerifyUserAsync(userId);
            if (!success)
            {
                return NotFound(new { message = "User not found or already verified" });
            }

            return Ok(new { message = "User verified successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during verification" });
        }
    }

    // POST /api/auth/onboard/recruiter
    // onboards recruiter and company in one step
    [HttpPost("onboard/recruiter")]
    public async Task<IActionResult> CompanyAndRecruiterRegistration([FromBody] RecruiterOnboardingDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _authService.OnboardRecruiterAndCompanyAsync(dto);
            if (result == null)
                return StatusCode(500, "Onboarding failed.");
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            // Known business errors (company/user already exists)
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Unexpected errors
            return StatusCode(500, ex.Message);
        }
    }

    // Helper method to set refresh token in httpOnly cookie
    private void SetRefreshTokenCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,        // Prevents XSS attacks
            Secure = false,         // Set to true in production with HTTPS
            SameSite = SameSiteMode.Strict, // CSRF protection
            Expires = DateTime.UtcNow.AddDays(30), // Match refresh token expiry
            Path = "/"
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}

// helper dto for token requests
public class RefreshTokenRequest
{
    public string Token { get; set; } = string.Empty;
}

public class CheckStudentRequestDTO
{
    [Required]
    public string Email { get; set; } = string.Empty;
}

public class CheckStudentResponseDTO
{
    public bool IsValid { get; set; }
}