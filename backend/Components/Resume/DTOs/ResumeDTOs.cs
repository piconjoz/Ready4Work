using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace backend.Components.Resume.DTOs;

public class UploadResumeDto
{
    [Required]
    public IFormFile File { get; set; } = default!;
}

public class ResumeResponseDto
{
    public int ResumeId     { get; set; }
    public int ApplicantId  { get; set; }
    public string ResumePath { get; set; } = string.Empty;
    public string ResumeText { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}