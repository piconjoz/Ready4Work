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
public class ApplicantController : ControllerBase
{
    private readonly IApplicantService _applicantService;

    public ApplicantController(IApplicantService applicantService)
    {
        _applicantService = applicantService;
    }

    // GET /api/applicant/profile
    // gets current applicant's complete profile (user + applicant data)
    [HttpGet("profile")]
    public async Task<ActionResult<ApplicantResponseDTO>> GetProfile()
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            // check if user is actually an applicant
            if (GetUserTypeFromToken() != 1)
            {
                return Forbid("Access denied: Only applicants can access this endpoint");
            }

            var applicantProfile = await _applicantService.GetApplicantProfileAsync(userId.Value);
            if (applicantProfile == null)
            {
                return NotFound(new { message = "Applicant profile not found" });
            }

            return Ok(applicantProfile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving applicant profile" });
        }
    }

    // PUT /api/applicant/profile
    // updates current applicant's profile details
    [HttpPut("profile")]
    public async Task<ActionResult<ApplicantResponseDTO>> UpdateProfile([FromBody] UpdateApplicantRequest request)
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

            // check if user is actually an applicant
            if (GetUserTypeFromToken() != 1)
            {
                return Forbid("Access denied: Only applicants can access this endpoint");
            }

            // get applicant id first
            var applicant = await _applicantService.GetApplicantByUserIdAsync(userId.Value);
            if (applicant == null)
            {
                return NotFound(new { message = "Applicant profile not found" });
            }

            // update applicant details
            var updatedApplicant = await _applicantService.UpdateApplicantDetailsAsync(
                applicant.GetApplicantId(),
                request.ProgrammeId,
                request.AdmitYear
            );

            // return complete profile with user data
            var applicantProfile = await _applicantService.ConvertToResponseDTOAsync(updatedApplicant);
            return Ok(applicantProfile);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating applicant profile" });
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

// helper dto for applicant update request
public class UpdateApplicantRequest
{
    [Required(ErrorMessage = "Programme ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Programme ID must be a positive number")]
    public int ProgrammeId { get; set; }

    [Required(ErrorMessage = "Admit year is required")]
    [Range(2020, 2030, ErrorMessage = "Admit year must be between 2020 and 2030")]
    public int AdmitYear { get; set; }
}