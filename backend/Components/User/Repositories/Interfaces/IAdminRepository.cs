namespace backend.User.Repositories.Interfaces;

using backend.User.Models;

public interface IAdminRepository
{
    // basic crud operations
    Task<Admin?> GetByIdAsync(int adminId);
    Task<Admin?> GetByUserIdAsync(int userId);
    Task<Admin> CreateAsync(Admin admin);
    Task<Admin> UpdateAsync(Admin admin);
    Task<bool> DeleteAsync(int adminId);
    
    // utility methods
    Task<bool> ExistsByUserIdAsync(int userId);
    Task<List<Admin>> GetAllAdminsAsync();
}