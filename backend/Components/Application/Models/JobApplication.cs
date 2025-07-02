namespace backend.Components.Application.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class JobApplication
{
    [Key]
    [Column("application_id")]
    public int ApplicationId { get; set; }
    
    [Required]
    [Column("applicant_id")]
    public int ApplicantId { get; set; }
    
    [Required]
    [Column("job_id")]
    public int JobId { get; set; }
    
    [Required]
    [Column("cover_letter_id")]
    public int CoverLetterId { get; set; }  // FK to CoverLetter table (following ER diagram)
    
    [Required]
    [Column("status")]
    [StringLength(50)]
    public string Status { get; set; } = "pending";
    
    [Column("applied_date")]
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual backend.Components.CoverLetter.Models.CoverLetter? CoverLetter { get; set; }
    public virtual backend.User.Models.Applicant? Applicant { get; set; }

    // Default constructor for EF Core
    public JobApplication() { }

    // Constructor for services
    public JobApplication(int applicantId, int jobId, int coverLetterId, string status)
    {
        ApplicantId = applicantId;
        JobId = jobId;
        CoverLetterId = coverLetterId;
        Status = status;
        AppliedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}