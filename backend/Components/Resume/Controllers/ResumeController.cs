using Microsoft.AspNetCore.Mvc;
using backend.Components.Resume.DTOs;
using backend.Components.Resume.Services;

namespace backend.Components.Resume.Controllers;

[ApiController]
[Route("api/resumes")]
public class ResumeController : ControllerBase
{
    private readonly IResumeService _service;
    private readonly ILogger<ResumeController> _logger;

    public ResumeController(IResumeService service, ILogger<ResumeController> logger)
    {
        _service = service;
        _logger  = logger;
    }

    // POST api/resumes/upload
    [HttpPost("upload")]
    public async Task<ActionResult<ResumeResponseDto>> Upload([FromForm] UploadResumeDto dto)
    {
        int userId = 1; // TODO: replace with real auth
        _logger.LogInformation("User {UserId} uploading resume", userId);

        var result = await _service.UploadAsync(userId, dto);
        return Ok(result);
    }

    // GET api/resumes/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<ResumeResponseDto>>> List(int userId)
        => Ok(await _service.ListByApplicantAsync(userId));

    // DELETE api/resumes/{resumeId}
    [HttpDelete("{resumeId}")]
    public async Task<IActionResult> Delete(int resumeId)
    {
        await _service.DeleteAsync(resumeId);
        return NoContent();
    }
}