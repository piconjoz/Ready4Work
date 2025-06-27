namespace backend.User.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.User.DTOs;
using backend.User.Services.Interfaces;
using System.ComponentModel.DataAnnotations;


[ApiController]
[Route("api/recruiters")]
[Authorize] // all endpoints require authentication
public class RecruiterController : ControllerBase
{
    private readonly IRecruiterService _recruiterService;

    public RecruiterController(IRecruiterService recruiterService)
    {
        _recruiterService = recruiterService;
    }

    // GET /api/recruiter/profile
    // gets current recruiter's complete profile (user + recruiter data)
    [HttpGet("profile")]
    public async Task<ActionResult<RecruiterResponseDTO>> GetProfile()
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            // check if user is actually a recruiter
            if (GetUserTypeFromToken() != 2)
            {
                return Forbid("Access denied: Only recruiters can access this endpoint");
            }

            var recruiterProfile = await _recruiterService.GetRecruiterProfileAsync(userId.Value);
            if (recruiterProfile == null)
            {
                return NotFound(new { message = "Recruiter profile not found" });
            }

            return Ok(recruiterProfile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving recruiter profile" });
        }
    }

    // PUT /api/recruiter/profile
    // updates current recruiter's profile details
    [HttpPut("profile")]
    public async Task<ActionResult<RecruiterResponseDTO>> UpdateProfile([FromBody] UpdateRecruiterRequest request)
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

            // check if user is actually a recruiter
            if (GetUserTypeFromToken() != 2)
            {
                return Forbid("Access denied: Only recruiters can access this endpoint");
            }

            // get recruiter id first
            var recruiter = await _recruiterService.GetRecruiterByUserIdAsync(userId.Value);
            if (recruiter == null)
            {
                return NotFound(new { message = "Recruiter profile not found" });
            }

            // update recruiter details
            var updatedRecruiter = await _recruiterService.UpdateRecruiterDetailsAsync(
                recruiter.GetRecruiterId(),
                request.CompanyId,
                request.JobTitle,
                request.Department
            );

            // return complete profile with user data
            var recruiterProfile = await _recruiterService.ConvertToResponseDTOAsync(updatedRecruiter);
            return Ok(recruiterProfile);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating recruiter profile" });
        }
    }

    // GET /api/recruiter/company/{companyId}
    // gets all recruiters for a company (admin feature)
    [HttpGet("company/{companyId}")]
    public async Task<ActionResult<List<RecruiterResponseDTO>>> GetRecruitersByCompany(int companyId)
    {
        try
        {
            // only admins or company managers should access this
            // for now, just check if user is authenticated
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            if (companyId <= 0)
            {
                return BadRequest(new { message = "Invalid company ID" });
            }

            var recruiters = await _recruiterService.GetRecruitersByCompanyAsync(companyId);
            var recruiterDtos = new List<RecruiterResponseDTO>();

            foreach (var recruiter in recruiters)
            {
                var dto = await _recruiterService.ConvertToResponseDTOAsync(recruiter);
                recruiterDtos.Add(dto);
            }

            return Ok(recruiterDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving company recruiters" });
        }
    }

    // helper methods to extract claims from jwt token
    private int? GetUserIdFromToken()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }
        return null;
    }

    private int? GetUserTypeFromToken()
    {
        var userTypeClaim = User.FindFirst("user_type");
        if (userTypeClaim != null && int.TryParse(userTypeClaim.Value, out int userType))
        {
            return userType;
        }
        return null;
    }
}

// helper dto for recruiter update request
public class UpdateRecruiterRequest
{
    [Required(ErrorMessage = "Company ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Company ID must be a positive number")]
    public int CompanyId { get; set; }

    [Required(ErrorMessage = "Job title is required")]
    [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
    public string JobTitle { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
    public string? Department { get; set; }
}