using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.JobListing.Models;
public class JobListing
{
    [Key]
    [Column("job_id")]
    public int JobId { get; set; }

    [Required]
    [Column("recruiter_id")]
    public int RecruiterId { get; set; }

    [Column("job_requirements")]
    public string JobRequirements { get; set; } = string.Empty;

    [Column("job_description")]
    public string JobDescription { get; set; } = string.Empty;

    [Column("listing_name")]
    public string ListingName { get; set; } = string.Empty;

    [Column("deadline")]
    public DateTime Deadline { get; set; }

    [Column("max_vacancies")]
    public int MaxVacancies { get; set; }

    [Column("is_visible")]
    public bool IsVisible { get; set; } = true;

    [Column("remumeration_type")]
    public string RemunerationType { get; set; } = string.Empty;

    [Column("job_duration")]
    public string JobDuration { get; set; } = string.Empty;

    [Column("rate")]
    public float Rate { get; set; }

    [Column("working_hours")]
    public string WorkingHours { get; set; } = string.Empty;

    [Column("job_scheme")]
    public string JobScheme { get; set; } = string.Empty;

    [Column("permitted_qualifications")]
    public int PermittedQualifications { get; set; }

    [Column("skillsets")]
    public int Skillsets { get; set; }

    [Column("job_status")]
    public string JobStatus { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public JobListing() { }

    internal JobListing(int recruiterId, string jobRequirements, string jobDesription, string listingName, DateTime deadline, int maxVacancies, bool isVisble, string remunerationType, string jobDuration, float rate, string workingHours, string jobScheme, int permittedQualifications, int skillsets, string jobStatus)
    {
        RecruiterId = recruiterId;
        JobRequirements = jobRequirements;
        JobDescription = jobDesription;
        ListingName = listingName;
        Deadline = deadline;
        MaxVacancies = maxVacancies;
        IsVisible = isVisble;
        RemunerationType = remunerationType;
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
    internal string GetRemunerationType() => RemunerationType;
    internal string GetJobDuration() => JobDuration;
    internal float GetRate() => Rate;
    internal string GetWorkingHours() => WorkingHours;
    internal string GetJobScheme() => JobScheme;
    internal int GetPermittedQualifications() => PermittedQualifications;
    internal int GetSkillsets() => Skillsets;
    internal string GetJobStatus() => JobStatus;
    internal DateTime GetCreatedAt() => CreatedAt;
    internal DateTime GetUpdatedAt() => UpdatedAt;

    // setters
    // for security cannot modify JobId and RecruiterId for access control
    // internal void SetJobId(int jobId) => JobId = jobId;
    // internal void SetRecruiterId(int recruiterId) => RecruiterId = recruiterId;
    internal void SetJobRequirements(string jobRequirements) => JobRequirements = jobRequirements;
    internal void SetJobDescription(string jobDesription) => JobDescription = jobDesription;
    internal void SetListingName(string listingName) => ListingName = listingName;
    internal void SetDeadline(DateTime deadline) => Deadline = deadline;
    internal void SetMaxVacancies(int maxVacancies) => MaxVacancies = maxVacancies;
    internal void SetIsVisible(bool isVisble) => IsVisible = isVisble;
    internal void SetRemunerationType(string remunerationType) => RemunerationType = remunerationType;
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
