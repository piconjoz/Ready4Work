namespace backend.User.Services.Interfaces;

using backend.User.Models;
using backend.User.DTOs;

public interface IApplicantService
{
    Task<Applicant> CreateApplicantAsync(int userId, int programmeId, int admitYear);
    Task<Applicant?> GetApplicantByIdAsync(int applicantId);
    Task<Applicant?> GetApplicantByUserIdAsync(int userId);

    Task<Applicant> UpdateApplicantDetailsAsync(int applicantId, int programmeId, int admitYear);
    Task<bool> DeleteApplicantAsync(int applicantId);
    
    // sample business logic methods (might not be needed)
    Task<bool> ExistsByUserIdAsync(int userId);
    Task<List<Applicant>> GetApplicantsByProgrammeAsync(int programmeId);
    Task<List<Applicant>> GetApplicantsByYearAsync(int admitYear);
    
    // DTO methods w/ user data
    Task<ApplicantResponseDTO> ConvertToResponseDTOAsync(Applicant applicant);
    Task<ApplicantResponseDTO> GetApplicantProfileAsync(int userId);
}