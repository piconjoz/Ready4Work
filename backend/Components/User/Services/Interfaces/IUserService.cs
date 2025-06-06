namespace backend.User.Services.Interfaces;

using backend.User.Models;
using backend.User.DTOs;

public interface IUserService
{
    // user creation and management
    Task<User> CreateUserAsync(string nric, string email, string firstName, string lastName, 
        string? phone, string? gender, int userType, string password);
    
    // user retrieval
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByNricAsync(string nric);
    
    // user validation and authentication
    Task<bool> ValidateUserCredentialsAsync(string email, string password);
    Task<User?> AuthenticateUserAsync(string email, string password);
    
    // user management
    Task<User> UpdateUserDetailsAsync(int userId, string firstName, string lastName, string? phone, string? gender);
    Task<User> UpdateUserPasswordAsync(int userId, string newPassword);
    Task<User> VerifyUserAsync(int userId);
    Task<User> DeactivateUserAsync(int userId);
    Task<User> UpdateProfilePictureAsync(int userId, string profilePicturePath);
    
    // utility methods
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NricExistsAsync(string nric);
    
    // dto conversion
    UserResponseDTO ConvertToResponseDTO(User user);
}