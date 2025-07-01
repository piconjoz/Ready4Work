namespace backend.Components.Resume.Models;

public class Resume
{
    public int ResumeId { get; set; }           // PK
    public int ApplicantId { get; set; }        // FK → your Applicant table
    public string ResumePath { get; set; } = string.Empty;   
    public string ResumeText { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }    // timestamp
}