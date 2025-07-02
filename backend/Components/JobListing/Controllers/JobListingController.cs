namespace backend.Components.JobListing.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Components.JobListing.Services.Interfaces;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using backend.User.Services.Interfaces;
using backend.Components.JobListing.DTOs;

[ApiController]
[Route("api/jobListings")]
[Authorize]
public class JobListingController : ControllerBase
{
    private readonly IJobListingService _jobListingService;
    private readonly IRecruiterService _recruiterService;

    public JobListingController(IJobListingService jobListingService, IRecruiterService recruiterService)
    {
        _jobListingService = jobListingService;
        _recruiterService = recruiterService;
    }

    // GET /api/jobListing/listings
    // get the relevant information for the recruiter listing page
    [HttpGet("listings")]
    public async Task<ActionResult<List<JobListingResponseDTO>>> GetRecruiterListings()
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            var recruiter = await _recruiterService.GetRecruiterByUserIdAsync(userId.Value);
            if (recruiter == null)
            {
                return NotFound(new { message = "Recruiter profile not found" });
            }
            var recruiterId = recruiter.GetRecruiterId();

            var recruiterListings = await _jobListingService.GetAllRecruiterJobListingsAsync(recruiterId);
            if (!recruiterListings.Any())
            {
                return Ok(new List<JobListingResponseDTO>()); // Return empty list
            }
            return Ok(recruiterListings);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // return StatusCode(500, new { message = "An error occurred while fetching job listings" });
            Console.WriteLine($"[ERROR] {ex.Message}\n{ex.StackTrace}"); // ✅ Log to console
            return StatusCode(500, new { message = "An error occurred while fetching job listings" });
        }
    }
    
    // helper for token access
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