namespace backend.User.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.User.DTOs;
using backend.User.Services.Interfaces;
using System.ComponentModel.DataAnnotations;


[ApiController]
[Route("api/[controller]")]
[Authorize] // all endpoints require authentication
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET /api/user/profile
    // gets current user's profile data
    [HttpGet("profile")]
    public async Task<ActionResult<UserResponseDTO>> GetProfile()
    {
        try
        {
            // get user id from jwt token
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var user = await _userService.GetUserByIdAsync(userId.Value);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var userDto = _userService.ConvertToResponseDTO(user);
            return Ok(userDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving profile" });
        }
    }

    // PUT /api/user/profile
    // updates current user's profile details
    [HttpPut("profile")]
    public async Task<ActionResult<UserResponseDTO>> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var updatedUser = await _userService.UpdateUserDetailsAsync(
                userId.Value,
                request.FirstName,
                request.LastName,
                request.Phone,
                request.Gender
            );

            var userDto = _userService.ConvertToResponseDTO(updatedUser);
            return Ok(userDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating profile" });
        }
    }

    // PUT /api/user/password
    // updates current user's password
    [HttpPut("password")]
    public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            await _userService.UpdateUserPasswordAsync(userId.Value, request.NewPassword);
            return Ok(new { message = "Password updated successfully" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating password" });
        }
    }

    // PUT /api/user/profile-picture
    // updates current user's profile picture
    [HttpPut("profile-picture")]
    public async Task<ActionResult<UserResponseDTO>> UpdateProfilePicture([FromBody] UpdateProfilePictureRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var updatedUser = await _userService.UpdateProfilePictureAsync(userId.Value, request.ProfilePicturePath);
            var userDto = _userService.ConvertToResponseDTO(updatedUser);
            return Ok(userDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating profile picture" });
        }
    }

    // helper method to extract user id from jwt token
    private int? GetUserIdFromToken()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }
        return null;
    }
}

// helper dtos for requests
public class UpdateProfileRequest
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string LastName { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
    public string? Phone { get; set; }

    [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters")]
    public string? Gender { get; set; }
}

public class UpdatePasswordRequest
{
    [Required(ErrorMessage = "New password is required")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 255 characters")]
    public string NewPassword { get; set; } = string.Empty;
}

public class UpdateProfilePictureRequest
{
    [Required(ErrorMessage = "Profile picture path is required")]
    [StringLength(255, ErrorMessage = "Profile picture path cannot exceed 255 characters")]
    public string ProfilePicturePath { get; set; } = string.Empty;
}