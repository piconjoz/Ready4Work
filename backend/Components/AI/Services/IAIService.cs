namespace backend.Components.AI.Services;

public interface IAIService
{
    Task<string> GenerateCoverLetterAsync(string resumeKeywords, string jobTitle, string jobDescription, string applicantName);
}