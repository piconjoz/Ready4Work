using backend.Components.User.Models;

namespace backend.Components.User.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    // basic crud operations
    Task<RefreshToken?> GetByIdAsync(int refreshTokenId);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
    Task<RefreshToken> UpdateAsync(RefreshToken refreshToken);
    Task<bool> DeleteAsync(int refreshTokenId);
    
    // utility methods
    Task<bool> ExistsByTokenAsync(string token);
    Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(int userId);
    Task<List<RefreshToken>> GetExpiredTokensAsync();
    Task RevokeAllUserTokensAsync(int userId);
    Task<int> CleanupExpiredTokensAsync();
}