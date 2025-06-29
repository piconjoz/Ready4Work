namespace backend.Components.CoverLetter.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Components.CoverLetter.Services;

[ApiController]
[Route("api/cover-letters")]
public class CoverLetterController : ControllerBase
{
    private readonly ICoverLetterService _coverLetterService;
    private readonly ILogger<CoverLetterController> _logger;

    public CoverLetterController(ICoverLetterService coverLetterService, ILogger<CoverLetterController> logger)
    {
        _coverLetterService = coverLetterService;
        _logger = logger;
    }

    // GET api/cover-letters/{coverLetterId}/download
    [HttpGet("{coverLetterId}/download")]
    public async Task<IActionResult> DownloadCoverLetter(int coverLetterId)
    {
        try
        {
            _logger.LogInformation("Downloading cover letter {CoverLetterId}", coverLetterId);

            var pdfBytes = await _coverLetterService.GetCoverLetterPdfAsync(coverLetterId);
            var fileName = $"cover_letter_{coverLetterId}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Cover letter not found: {CoverLetterId}", coverLetterId);
            return NotFound($"Cover letter not found: {ex.Message}");
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError("Cover letter PDF file not found: {Message}", ex.Message);
            return NotFound("Cover letter PDF file not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error downloading cover letter {CoverLetterId}", coverLetterId);
            return StatusCode(500, "An error occurred while downloading the cover letter");
        }
    }

    // GET api/cover-letters/test
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new { message = "CoverLetter Controller is working!", timestamp = DateTime.UtcNow });
    }
}