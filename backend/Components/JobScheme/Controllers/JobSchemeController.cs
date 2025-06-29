namespace backend.JobScheme.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.JobScheme.DTOs;
using backend.JobScheme.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/job-schemes")]
[Authorize(Roles = "Admin")] // Only admin users can manage job schemes
public class JobSchemeController : ControllerBase
{
    private readonly IJobSchemeService _jobSchemeService;

    public JobSchemeController(IJobSchemeService jobSchemeService)
    {
        _jobSchemeService = jobSchemeService;
    }

    // GET /api/job-schemes
    // Gets all job schemes
    [HttpGet]
    public async Task<ActionResult<List<JobSchemeResponseDTO>>> GetAllJobSchemes()
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var jobSchemes = await _jobSchemeService.GetAllJobSchemeDTOsAsync();
            return Ok(jobSchemes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving job schemes" });
        }
    }

    // GET /api/job-schemes/{id}
    // Gets a specific job scheme by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<JobSchemeResponseDTO>> GetJobScheme(int id)
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var jobScheme = await _jobSchemeService.GetJobSchemeByIdAsync(id);
            if (jobScheme == null)
            {
                return NotFound(new { message = "Job scheme not found" });
            }

            var jobSchemeDTO = _jobSchemeService.ConvertToResponseDTO(jobScheme);
            return Ok(jobSchemeDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the job scheme" });
        }
    }

    // POST /api/job-schemes
    // Creates a new job scheme
    [HttpPost]
    public async Task<ActionResult<JobSchemeResponseDTO>> CreateJobScheme([FromBody] CreateJobSchemeRequest request)
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

            var jobScheme = await _jobSchemeService.CreateJobSchemeAsync(request.SchemeName);
            var jobSchemeDTO = _jobSchemeService.ConvertToResponseDTO(jobScheme);
            
            return CreatedAtAction(nameof(GetJobScheme), new { id = jobSchemeDTO.SchemeId }, jobSchemeDTO);
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
            return StatusCode(500, new { message = "An error occurred while creating the job scheme" });
        }
    }

    // PUT /api/job-schemes/{id}
    // Updates an existing job scheme
    [HttpPut("{id}")]
    public async Task<ActionResult<JobSchemeResponseDTO>> UpdateJobScheme(int id, [FromBody] UpdateJobSchemeRequest request)
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

            var jobScheme = await _jobSchemeService.UpdateJobSchemeAsync(id, request.SchemeName);
            var jobSchemeDTO = _jobSchemeService.ConvertToResponseDTO(jobScheme);
            
            return Ok(jobSchemeDTO);
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
            return StatusCode(500, new { message = "An error occurred while updating the job scheme" });
        }
    }

    // DELETE /api/job-schemes/{id}
    // Deletes a job scheme
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteJobScheme(int id)
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var result = await _jobSchemeService.DeleteJobSchemeAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Job scheme not found" });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the job scheme" });
        }
    }

    // Helper methods to extract claims from JWT token
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

// Helper DTOs for request validation
public class CreateJobSchemeRequest
{
    [Required(ErrorMessage = "Scheme name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Scheme name must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9\s\-_]+$", ErrorMessage = "Scheme name must contain only letters, numbers, spaces, hyphens, and underscores")]
    public string SchemeName { get; set; }
}

public class UpdateJobSchemeRequest
{
    [Required(ErrorMessage = "Scheme name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Scheme name must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9\s\-_]+$", ErrorMessage = "Scheme name must contain only letters, numbers, spaces, hyphens, and underscores")]
    public string SchemeName { get; set; }
}