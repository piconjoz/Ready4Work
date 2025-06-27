namespace backend.User.Services.Interfaces;

using backend.User.DTOs;

public interface IAuthService
{
    // signup methods - separate endpoints for security
    Task<AuthResponseDTO> SignupApplicantAsync(ApplicantSignupDTO signupDto);
    Task<AuthResponseDTO> SignupRecruiterAsync(RecruiterSignupDTO signupDto);
    
    // login method - unified for all user types
    Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
    
    // token management
    Task<AuthResponseDTO?> RefreshTokenAsync(string token);
    Task<bool> ValidateTokenAsync(string token);
    
    // user verification (for email verification workflow)
    Task<bool> VerifyUserAsync(int userId);
}