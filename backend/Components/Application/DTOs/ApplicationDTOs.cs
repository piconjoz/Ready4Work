namespace backend.Components.Application.DTOs;

using System.ComponentModel.DataAnnotations;

public class SubmitApplicationDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Valid job listing ID is required")]
    public int JobId { get; set; }
}

public class ApplicationResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int ApplicationId { get; set; }
    public bool CoverLetterGenerated { get; set; }
}