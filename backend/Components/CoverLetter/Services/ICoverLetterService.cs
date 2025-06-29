namespace backend.Components.CoverLetter.Services;

using backend.Components.CoverLetter.Models;

public interface ICoverLetterService
{
    Task<CoverLetter> GenerateAndSaveCoverLetterAsync(string content, string applicantName, string jobTitle, int applicantId);
    Task<byte[]> GetCoverLetterPdfAsync(int coverLetterId);
    Task<string> GetCoverLetterDownloadUrlAsync(int coverLetterId);
    Task<bool> DeleteCoverLetterAsync(int coverLetterId);
}
