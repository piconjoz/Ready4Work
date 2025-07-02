namespace backend.Components.User.Services;

using System.Security.Cryptography;
using backend.Components.User.Models;
using backend.Components.User.Repositories.Interfaces;
using backend.Components.User.Services.Interfaces;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<string> GenerateRefreshTokenAsync(int userId, int expirationDays = 30)
    {
        if (userId <= 0) throw new ArgumentException("Invalid user ID");
        if (expirationDays <= 0) throw new ArgumentException("Expiration days must be positive");

        // Generate cryptographically secure random token
        var tokenBytes = new byte[32]; // 256 bits
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(tokenBytes);
        }
        var token = Convert.ToBase64String(tokenBytes);

        // Create refresh token entity
        var expiresAt = DateTime.UtcNow.AddDays(expirationDays);
        var refreshToken = new RefreshToken(userId, token, expiresAt);

        // Save to database
        await _refreshTokenRepository.CreateAsync(refreshToken);

        return token;
    }

    public async Task<RefreshToken?> ValidateRefreshTokenAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return null;

        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
        
        // Check if token exists and is valid
        if (refreshToken == null || !refreshToken.IsValid())
        {
            return null;
        }

        return refreshToken;
    }

    public async Task<string> RotateRefreshTokenAsync(string oldToken)
    {
        if (string.IsNullOrWhiteSpace(oldToken)) 
            throw new ArgumentException("Old token cannot be empty");

        // Validate the old token
        var oldRefreshToken = await ValidateRefreshTokenAsync(oldToken);
        if (oldRefreshToken == null)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        // Revoke the old token
        oldRefreshToken.Revoke();
        await _refreshTokenRepository.UpdateAsync(oldRefreshToken);

        // Generate a new token for the same user
        var newToken = await GenerateRefreshTokenAsync(oldRefreshToken.GetUserId());

        return newToken;
    }

    public async Task RevokeRefreshTokenAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return;

        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
        if (refreshToken == null) return;

        refreshToken.Revoke();
        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    public async Task RevokeAllUserTokensAsync(int userId)
    {
        if (userId <= 0) return;

        await _refreshTokenRepository.RevokeAllUserTokensAsync(userId);
    }

    public async Task<int> CleanupExpiredTokensAsync()
    {
        return await _refreshTokenRepository.CleanupExpiredTokensAsync();
    }

    public async Task<bool> IsTokenValidAsync(string token)
    {
        var refreshToken = await ValidateRefreshTokenAsync(token);
        return refreshToken != null;
    }

    public async Task<int?> GetUserIdFromTokenAsync(string token)
    {
        var refreshToken = await ValidateRefreshTokenAsync(token);
        return refreshToken?.GetUserId();
    }
}