namespace backend.Components.Application.Services;

public interface IApplicationService
{
    Task<ApplicationResult> SubmitApplicationWithCoverLetterAsync(int applicantId, int jobListingId);
}

public class ApplicationResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string GeneratedCoverLetter { get; set; } = string.Empty;
    public int ApplicationId { get; set; }
}