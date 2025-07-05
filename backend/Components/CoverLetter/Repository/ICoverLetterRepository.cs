namespace backend.Components.CoverLetter.Repository;

using backend.Components.CoverLetter.Models;

public interface ICoverLetterRepository
{
    Task<CoverLetter?> GetByIdAsync(int coverLetterId);
    Task<List<CoverLetter>> GetByApplicantIdAsync(int applicantId);
    Task<CoverLetter> CreateAsync(CoverLetter coverLetter);
    Task<CoverLetter> UpdateAsync(CoverLetter coverLetter);
    Task<bool> DeleteAsync(int coverLetterId);
}