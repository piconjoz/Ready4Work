namespace backend.User.Services.Interfaces;

using backend.User.DTOs;
using backend.User.Models;

public interface IAdminAccountManagementService
{
    // CREATE
    Task<AccountManagementResponseDTO> CreateUserAccountAsync(CreateUserAccountDTO dto, int adminId);
    
    // READ
    Task<List<AccountManagementResponseDTO>> GetAllUsersAsync();
    Task<AccountManagementResponseDTO?> GetUserAccountAsync(int userId);
    Task<List<AccountManagementResponseDTO>> GetUsersByTypeAsync(int userType);
    
    // UPDATE
    Task<AccountManagementResponseDTO> UpdateUserAccountAsync(int userId, UpdateUserAccountDTO dto, int adminId);
    
    // DELETE
    Task<bool> DeleteUserAccountAsync(int userId, int adminId);
    
    // Helper methods
    Task<bool> EmailExistsAsync(string email);
}