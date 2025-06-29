namespace backend.Components.User.Repositories;

using backend.Components.User.Models;
using backend.Components.User.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Data;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByIdAsync(int refreshTokenId)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => EF.Property<int>(rt, "RefreshTokenId") == refreshTokenId);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => EF.Property<string>(rt, "Token") == token);
    }

    public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<RefreshToken> UpdateAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<bool> DeleteAsync(int refreshTokenId)
    {
        var refreshToken = await GetByIdAsync(refreshTokenId);
        if (refreshToken == null) return false;

        _context.RefreshTokens.Remove(refreshToken);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByTokenAsync(string token)
    {
        return await _context.RefreshTokens.AnyAsync(rt => EF.Property<string>(rt, "Token") == token);
    }

    public async Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(int userId)
    {
        return await _context.RefreshTokens
            .Where(rt => EF.Property<int>(rt, "UserId") == userId && 
                        EF.Property<bool>(rt, "IsRevoked") == false &&
                        EF.Property<DateTime>(rt, "ExpiresAt") > DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<List<RefreshToken>> GetExpiredTokensAsync()
    {
        return await _context.RefreshTokens
            .Where(rt => EF.Property<DateTime>(rt, "ExpiresAt") <= DateTime.UtcNow ||
                        EF.Property<bool>(rt, "IsRevoked") == true)
            .ToListAsync();
    }

    public async Task RevokeAllUserTokensAsync(int userId)
    {
        var userTokens = await _context.RefreshTokens
            .Where(rt => EF.Property<int>(rt, "UserId") == userId && 
                        EF.Property<bool>(rt, "IsRevoked") == false)
            .ToListAsync();

        foreach (var token in userTokens)
        {
            token.Revoke();
        }

        await _context.SaveChangesAsync();
    }

    public async Task<int> CleanupExpiredTokensAsync()
    {
        var expiredTokens = await GetExpiredTokensAsync();
        
        if (expiredTokens.Any())
        {
            _context.RefreshTokens.RemoveRange(expiredTokens);
            await _context.SaveChangesAsync();
        }

        return expiredTokens.Count;
    }
}