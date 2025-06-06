namespace backend.User.Services.Interfaces;

using backend.User.Models;
using backend.User.DTOs;

public interface IRecruiterService
{
    // recruiter creation and retrieval
    Task<Recruiter> CreateRecruiterAsync(int userId, int companyId, string jobTitle, string? department);
    Task<Recruiter?> GetRecruiterByIdAsync(int recruiterId);
    Task<Recruiter?> GetRecruiterByUserIdAsync(int userId);
    
    // recruiter management
    Task<Recruiter> UpdateRecruiterDetailsAsync(int recruiterId, int companyId, string jobTitle, string? department);
    Task<bool> DeleteRecruiterAsync(int recruiterId);
    
    // business logic methods
    Task<bool> ExistsByUserIdAsync(int userId);
    Task<List<Recruiter>> GetRecruitersByCompanyAsync(int companyId);
    Task<List<Recruiter>> GetRecruitersByDepartmentAsync(string department);
    
    // dto conversion with user data
    Task<RecruiterResponseDTO> ConvertToResponseDTOAsync(Recruiter recruiter);
    Task<RecruiterResponseDTO> GetRecruiterProfileAsync(int userId);
}