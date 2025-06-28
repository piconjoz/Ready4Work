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

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
    // refreshes expired tokens
    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponseDTO>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return BadRequest(new { message = "Token is required" });
            }

            var result = await _authService.RefreshTokenAsync(request.Token);
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid or expired token" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during token refresh" });
        }
    }

    // POST /api/auth/validate
    // validates if token is still valid
    [HttpPost("validate")]
    public async Task<ActionResult<bool>> ValidateToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return BadRequest(new { message = "Token is required" });
            }

            var isValid = await _authService.ValidateTokenAsync(request.Token);
            return Ok(new { isValid });
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

    [HttpPost("onboard/recruiter")]
    public async Task<IActionResult> CompanyAndRecruiterRegistration([FromBody] RecruiterOnboardingDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.OnboardRecruiterAndCompanyAsync(dto);
        if (result == null)
            return StatusCode(500, "Onboarding failed.");
        return Ok(result);
    }
}

// helper dto for token requests
public class RefreshTokenRequest
{
    public string Token { get; set; } = string.Empty;
}
