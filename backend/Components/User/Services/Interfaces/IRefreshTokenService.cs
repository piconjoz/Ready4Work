namespace backend.Components.User.Services.Interfaces;

using backend.Components.User.Models;

public interface IRefreshTokenService
{
    // core refresh token operations
    Task<string> GenerateRefreshTokenAsync(int userId, int expirationDays = 30);
    Task<RefreshToken?> ValidateRefreshTokenAsync(string token);
    Task<string> RotateRefreshTokenAsync(string oldToken);
    
    // token management
    Task RevokeRefreshTokenAsync(string token);
    Task RevokeAllUserTokensAsync(int userId);
    Task<int> CleanupExpiredTokensAsync();
    
    // utility methods
    Task<bool> IsTokenValidAsync(string token);
    Task<int?> GetUserIdFromTokenAsync(string token);
}