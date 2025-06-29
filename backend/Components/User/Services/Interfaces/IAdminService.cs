namespace backend.User.Services.Interfaces;

using backend.User.Models;
using backend.User.DTOs;

public interface IAdminService
{
    // Admin creation and retrieval
    Task<Admin> CreateAdminAsync(int userId);
    Task<Admin?> GetAdminByIdAsync(int adminId);
    Task<Admin?> GetAdminByUserIdAsync(int userId);
    
    // Admin management
    Task<Admin> UpdateAdminAsync(int adminId);
    Task<bool> DeleteAdminAsync(int adminId);
    
    // Business logic methods
    Task<bool> ExistsByUserIdAsync(int userId);
    Task<List<Admin>> GetAllAdminsAsync();
    
    // DTO conversion with user data
    Task<AdminResponseDTO> ConvertToResponseDTOAsync(Admin admin);
    Task<AdminResponseDTO?> GetAdminProfileAsync(int userId);
    
    // Complete admin account creation (for seeding)
    Task<AdminResponseDTO> CreateCompleteAdminAccountAsync(
        string email, 
        string firstName, 
        string lastName, 
        string? phone, 
        string? gender, 
        string password);
}