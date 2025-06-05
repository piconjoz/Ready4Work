namespace backend.User.Repositories.Interfaces;

using backend.User.Models;

public interface IUserRepository
{
    // basic crud operations
    Task<User?> GetByIdAsync(int userId);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByNricAsync(string nric);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(int userId);
    
    // utility methods
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByNricAsync(string nric);
    Task<List<User>> GetActiveUsersAsync();
    Task<List<User>> GetUsersByTypeAsync(int userType);
}