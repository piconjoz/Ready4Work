namespace backend.User.Repositories.Interfaces;

using backend.User.Models;

public interface IApplicantRepository
{
    // basic crud operations
    Task<Applicant?> GetByIdAsync(int applicantId);
    Task<Applicant?> GetByUserIdAsync(int userId);
    Task<Applicant> CreateAsync(Applicant applicant);
    Task<Applicant> UpdateAsync(Applicant applicant);
    Task<bool> DeleteAsync(int applicantId);
    
    // utility methods
    Task<bool> ExistsByUserIdAsync(int userId);
    Task<List<Applicant>> GetByProgrammeIdAsync(int programmeId);
    Task<List<Applicant>> GetByAdmitYearAsync(int admitYear);
}