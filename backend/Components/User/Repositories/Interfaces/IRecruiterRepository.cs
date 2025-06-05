namespace backend.User.Repositories.Interfaces;

using backend.User.Models;

public interface IRecruiterRepository
{
    // basic crud operations
    Task<Recruiter?> GetByIdAsync(int recruiterId);
    Task<Recruiter?> GetByUserIdAsync(int userId);
    Task<Recruiter> CreateAsync(Recruiter recruiter);
    Task<Recruiter> UpdateAsync(Recruiter recruiter);
    Task<bool> DeleteAsync(int recruiterId);
    
    // utility methods
    Task<bool> ExistsByUserIdAsync(int userId);
    Task<List<Recruiter>> GetByCompanyIdAsync(int companyId);
    Task<List<Recruiter>> GetByDepartmentAsync(string department);
}