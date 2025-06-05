namespace backend.User.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Applicant
{
    [Key]
    [Column("applicant_id")]
    private int ApplicantId { get; set; }
    
    [Required]
    [Column("user_id")]
    private int UserId { get; set; }
    
    [Required]
    [Column("programme_id")]
    private int ProgrammeId { get; set; }
    
    [Required]
    [Column("admit_year")]
    private int AdmitYear { get; set; }
    
    [Column("created_at")]
    private DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    private DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    private Applicant() {}

    internal Applicant(int userId, int programmeId, int admitYear)
    {
        UserId = userId;
        ProgrammeId = programmeId;
        AdmitYear = admitYear;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    // internal getter methods - only services can read data
    internal int GetApplicantId() => ApplicantId;
    internal int GetUserId() => UserId;
    internal int GetProgrammeId() => ProgrammeId;
    internal int GetAdmitYear() => AdmitYear;
    internal DateTime GetCreatedAt() => CreatedAt;
    internal DateTime GetUpdatedAt() => UpdatedAt;
    
    // internal setter methods
    internal void SetApplicantId(int applicantId) => ApplicantId = applicantId;
    internal void SetProgrammeId(int programmeId) => ProgrammeId = programmeId;
    internal void SetAdmitYear(int admitYear) => AdmitYear = admitYear;
    internal void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
}