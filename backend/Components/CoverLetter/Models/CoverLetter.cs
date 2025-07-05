namespace backend.Components.CoverLetter.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CoverLetter
{
    [Key]
    [Column("cover_letter_id")]
    public int CoverLetterId { get; set; }
    
    [Required]
    [Column("applicant_id")]
    public int ApplicantId { get; set; }
    
    [Required]
    [Column("cover_letter_path")]
    [StringLength(1000)]
    public string CoverLetterPath { get; set; } = string.Empty;
    
    // Keep original AI text for reference/regeneration
    [Column("original_text")]
    public string OriginalText { get; set; } = string.Empty;
    
    [Column("file_size")]
    public long FileSize { get; set; }
    
    [Column("content_type")]
    [StringLength(50)]
    public string ContentType { get; set; } = "application/pdf";
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Default constructor for EF Core
    public CoverLetter() { }

    // Constructor for services
    public CoverLetter(int applicantId, string coverLetterPath, string originalText, long fileSize)
    {
        ApplicantId = applicantId;
        CoverLetterPath = coverLetterPath;
        OriginalText = originalText;
        FileSize = fileSize;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}