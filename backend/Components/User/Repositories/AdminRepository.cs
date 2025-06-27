namespace backend.User.Repositories;

using backend.User.Models;
using backend.User.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Data;

public class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _context;

    public AdminRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Admin?> GetByIdAsync(int adminId)
    {
        // use ef.property instead of a.GetAdminId()
        return await _context.Admins.FirstOrDefaultAsync(a => EF.Property<int>(a, "AdminId") == adminId);
    }

    public async Task<Admin?> GetByUserIdAsync(int userId)
    {
        // use ef.property instead of a.GetUserId()
        return await _context.Admins.FirstOrDefaultAsync(a => EF.Property<int>(a, "UserId") == userId);
    }

    public async Task<Admin> CreateAsync(Admin admin)
    {
        _context.Admins.Add(admin);
        await _context.SaveChangesAsync();
        return admin;
    }

    public async Task<Admin> UpdateAsync(Admin admin)
    {
        admin.SetUpdatedAt(DateTime.UtcNow);
        _context.Admins.Update(admin);
        await _context.SaveChangesAsync();
        return admin;
    }

    public async Task<bool> DeleteAsync(int adminId)
    {
        var admin = await GetByIdAsync(adminId);
        if (admin == null) return false;

        _context.Admins.Remove(admin);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByUserIdAsync(int userId)
    {
        return await _context.Admins.AnyAsync(a => EF.Property<int>(a, "UserId") == userId);
    }

    public async Task<List<Admin>> GetAllAdminsAsync()
    {
        return await _context.Admins.ToListAsync();
    }
}