using Microsoft.AspNetCore.Mvc;
using backend.Components.Resume.DTOs;
using backend.Components.Resume.Services;
using Microsoft.AspNetCore.Authorization;

namespace backend.Components.Resume.Controllers;

[ApiController]
[Route("api/resumes")]
[Authorize]     
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
        var result = await _service.UploadAsync(dto.ApplicantId, dto);
         _logger.LogInformation("Applicant {ApplicantId} uploading resume", dto.ApplicantId);
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