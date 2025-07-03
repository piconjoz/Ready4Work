namespace backend.Components.JobListing.DTOs;

public class JobListingRecruiterShowAllDTO
{
    // public int JobId { get; set; }
    public string ListingName { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public int DaysRemaining { get; set; }
    public string Visibility { get; set; } = string.Empty;
    public int Pending { get; set; }
    public int Approved { get; set; }
    public int maxVacancies { get; set; }
    // public int TotalApplications { get; set; }

    // public int Rejected { get; set; }

    // public int RemainingSlots { get; set; } // MaxApplicants - Approved

}