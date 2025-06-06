namespace backend.User.Repositories;

using backend.User.Models;
using backend.User.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Data;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        // use ef.property instead of u.GetUserId()
        return await _context.Users.FirstOrDefaultAsync(u => EF.Property<int>(u, "UserId") == userId);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        // use ef.property instead of u.GetEmail()
        return await _context.Users.FirstOrDefaultAsync(u => EF.Property<string>(u, "Email") == email);
    }

    public async Task<User?> GetByNricAsync(string nric)
    {
        // use ef.property instead of u.GetNRIC()
        return await _context.Users.FirstOrDefaultAsync(u => EF.Property<string>(u, "NRIC") == nric);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        user.SetUpdatedAt(DateTime.UtcNow);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(int userId)
    {
        var user = await GetByIdAsync(userId);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => EF.Property<string>(u, "Email") == email);
    }

    public async Task<bool> ExistsByNricAsync(string nric)
    {
        return await _context.Users.AnyAsync(u => EF.Property<string>(u, "NRIC") == nric);
    }

    public async Task<List<User>> GetActiveUsersAsync()
    {
        return await _context.Users.Where(u => EF.Property<bool>(u, "IsActive") == true).ToListAsync();
    }

    public async Task<List<User>> GetUsersByTypeAsync(int userType)
    {
        return await _context.Users.Where(u => EF.Property<int>(u, "UserType") == userType).ToListAsync();
    }
}