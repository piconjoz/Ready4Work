namespace backend.Components.Application.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Components.Application.Services;
using backend.Components.Application.DTOs;
using backend.Components.Application.Repository;

[ApiController]
[Route("api/applications")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ILogger<ApplicationController> _logger;

    public ApplicationController(IApplicationService applicationService, IApplicationRepository applicationRepository, ILogger<ApplicationController> logger)
    {
        _applicationService = applicationService;
        _applicationRepository = applicationRepository;
        _logger = logger;
    }

    // POST api/applications/apply
    [HttpPost("apply")]
    public async Task<ActionResult<ApplicationResponseDto>> SubmitApplication(SubmitApplicationDto request)
    {
        try
        {
            int userId = 1; // Hardcoded for testing
            _logger.LogInformation("Processing application for user {UserId} to job {JobId}", userId, request.JobListingId);

            var result = await _applicationService.SubmitApplicationWithCoverLetterAsync(userId, request.JobListingId);

            if (!result.Success)
            {
                return BadRequest(new ApplicationResponseDto
                {
                    Success = false,
                    Message = result.Message
                });
            }

            return Ok(new ApplicationResponseDto
            {
                Success = true,
                Message = result.Message,
                ApplicationId = result.ApplicationId,
                CoverLetterGenerated = !string.IsNullOrEmpty(result.GeneratedCoverLetter)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing application submission");
            return StatusCode(500, new ApplicationResponseDto
            {
                Success = false,
                Message = "An unexpected error occurred. Please try again later."
            });
        }
    }

    // GET api/applications/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult> GetUserApplications(int userId)
    {
        try
        {
            var applications = await _applicationRepository.GetByApplicantIdAsync(userId);
            
            var result = applications.Select(app => new
            {
                ApplicationId = app.ApplicationId,
                JobListingId = app.JobListingId,
                CoverLetter = app.CoverLetterId,
                Status = app.Status,
                AppliedDate = app.AppliedDate,
                UpdatedAt = app.UpdatedAt
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving applications for user {UserId}", userId);
            return StatusCode(500, "An error occurred while retrieving applications.");
        }
    }

    // DELETE api/applications/user/{userId} - NEW METHOD
    [HttpDelete("user/{userId}")]
    public async Task<ActionResult> DeleteUserApplications(int userId)
    {
        try
        {
            var applications = await _applicationRepository.GetByApplicantIdAsync(userId);
            int deletedCount = 0;
            
            foreach (var app in applications)
            {
                await _applicationRepository.DeleteAsync(app.ApplicationId);
                deletedCount++;
            }

            _logger.LogInformation("Deleted {Count} applications for user {UserId}", deletedCount, userId);
            
            return Ok(new { 
                message = $"Successfully deleted {deletedCount} applications for user {userId}", 
                deletedCount = deletedCount,
                userId = userId
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting applications for user {UserId}", userId);
            return StatusCode(500, "Error deleting applications");
        }
    }

    // DELETE api/applications/reset - NEW METHOD
    [HttpDelete("reset")]
    public async Task<ActionResult> ResetAllApplications()
    {
        try
        {
            var applications = await _applicationRepository.GetByApplicantIdAsync(1);
            int deletedCount = 0;
            
            foreach (var app in applications)
            {
                await _applicationRepository.DeleteAsync(app.ApplicationId);
                deletedCount++;
            }

            _logger.LogInformation("Reset complete: deleted {Count} applications", deletedCount);
            
            return Ok(new { 
                message = "All applications cleared for testing", 
                deletedCount = deletedCount 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resetting applications");
            return StatusCode(500, "Error resetting applications");
        }
    }

    // GET api/applications/test
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new { message = "Application Controller is working!", timestamp = DateTime.UtcNow });
    }
}