using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.JobListing.Models;
public class JobListing
{
    [Key]
    [Column("job_id")]
    private int JobId { get; set; }

    [Required]
    [Column("recruiter_id")]
    private int RecruiterId { get; set; }

    [Column("job_requirements")]
    private string JobRequirements { get; set; } = string.Empty;

    [Column("job_description")]
    private string JobDescription { get; set; } = string.Empty;

    [Column("listing_name")]
    private string ListingName { get; set; } = string.Empty;

    [Column("deadline")]
    private DateTime Deadline { get; set; }

    [Column("max_vacancies")]
    private int MaxVacancies { get; set; }

    [Column("is_visible")]
    private bool IsVisible { get; set; } = true;

    [Column("renumeration_type")]
    private string RenumerationType { get; set; } = string.Empty;

    [Column("job_duration")]
    private string JobDuration { get; set; } = string.Empty;

    [Column("rate")]
    private float Rate { get; set; }

    [Column("working_hours")]
    private string WorkingHours { get; set; } = string.Empty;

    [Column("job_scheme")]
    private string JobScheme { get; set; } = string.Empty;

    [Column("permitted_qualifications")]
    private int PermittedQualifications { get; set; }

    [Column("skillsets")]
    private int Skillsets { get; set; }

    [Column("job_status")]
    private string JobStatus { get; set; } = string.Empty;

    [Column("created_at")]
    private DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    private DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    private JobListing() { }

    internal JobListing(int recruiterId, string jobRequirements, string jobDesription, string listingName, DateTime deadline, int maxVacancies, bool isVisble, string renumerationType, string jobDuration, float rate, string workingHours, string jobScheme, int permittedQualifications, int skillsets, string jobStatus)
    {
        RecruiterId = recruiterId;
        JobRequirements = jobRequirements;
        JobDescription = jobDesription;
        ListingName = listingName;
        Deadline = deadline;
        MaxVacancies = maxVacancies;
        IsVisible = isVisble;
        RenumerationType = renumerationType;
        JobDuration = jobDuration;
        Rate = rate;
        WorkingHours = workingHours;
        JobScheme = jobScheme;
        PermittedQualifications = permittedQualifications;
        Skillsets = skillsets;
        JobStatus = jobStatus;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

    }

    // getter
    internal int GetJobId() => JobId;
    internal int GetRecruiterId() => RecruiterId;
    internal string GetJobRequirements() => JobRequirements;
    internal string GetJobDescription() => JobDescription;
    internal string GetListingName() => ListingName;
    internal DateTime GetDeadline() => Deadline;
    internal int GetMaxVacancies() => MaxVacancies;
    internal bool GetIsVisible() => IsVisible;
    internal string GetRenumerationType() => RenumerationType;
    internal string GetJobDuration() => JobDuration;
    internal float GetRate() => Rate;
    internal string GetWorkingHours() => WorkingHours;
    internal string GetJobScheme() => JobScheme;
    internal int GetPermittedQualifications() => PermittedQualifications;
    internal int GetSkillsets() => Skillsets;
    internal string GetJobStatus() => JobStatus;
    internal DateTime GetCreatedAt() => CreatedAt;
    internal DateTime GetUpdatedAt() => UpdatedAt;

    // setter
    internal void SetJobId(int jobId) => JobId = jobId;
    // internal void SetRecruiterId(int recruiterId) => RecruiterId = recruiterId;
    internal void SetJobRequirements(string jobRequirements) => JobRequirements = jobRequirements;
    internal void SetJobDescription(string jobDesription) => JobDescription = jobDesription;
    internal void SetListingName(string listingName) => ListingName = listingName;
    internal void SetDeadline(DateTime deadline) => Deadline = deadline;
    internal void SetMaxVacancies(int maxVacancies) => MaxVacancies = maxVacancies;
    internal void SetIsVisible(bool isVisble) => IsVisible = isVisble;
    internal void SetRenumerationType(string renumerationType) => RenumerationType = renumerationType;
    internal void SetJobDuration(string jobDuration) => JobDuration = jobDuration;
    internal void SetRate(float rate) => Rate = rate;
    internal void SetWorkingHours(string workingHours) => WorkingHours = workingHours;
    internal void SetJobScheme(string jobScheme) => JobScheme = jobScheme;
    internal void SetPermittedQualifications(int permittedQualifications) => PermittedQualifications = permittedQualifications;
    internal void SetSkillsets(int skillsets) => Skillsets =skillsets;
    internal void SetJobStatus(string jobStatus) => JobStatus = jobStatus;
    internal void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;
    internal void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
}
