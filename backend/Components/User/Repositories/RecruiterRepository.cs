namespace backend.User.Repositories;

using backend.User.Models;
using backend.User.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Data;

public class RecruiterRepository : IRecruiterRepository
{
    private readonly ApplicationDbContext _context;

    public RecruiterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Recruiter?> GetByIdAsync(int recruiterId)
    {
        return await _context.Recruiters.FirstOrDefaultAsync(r => r.GetRecruiterId() == recruiterId);
    }

    public async Task<Recruiter?> GetByUserIdAsync(int userId)
    {
        return await _context.Recruiters.FirstOrDefaultAsync(r => r.GetUserId() == userId);
    }

    public async Task<Recruiter> CreateAsync(Recruiter recruiter)
    {
        _context.Recruiters.Add(recruiter);
        await _context.SaveChangesAsync();
        return recruiter;
    }

    public async Task<Recruiter> UpdateAsync(Recruiter recruiter)
    {
        recruiter.SetUpdatedAt(DateTime.UtcNow);
        _context.Recruiters.Update(recruiter);
        await _context.SaveChangesAsync();
        return recruiter;
    }

    public async Task<bool> DeleteAsync(int recruiterId)
    {
        var recruiter = await GetByIdAsync(recruiterId);
        if (recruiter == null) return false;

        _context.Recruiters.Remove(recruiter);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByUserIdAsync(int userId)
    {
        return await _context.Recruiters.AnyAsync(r => r.GetUserId() == userId);
    }

    public async Task<List<Recruiter>> GetByCompanyIdAsync(int companyId)
    {
        return await _context.Recruiters.Where(r => r.GetCompanyId() == companyId).ToListAsync();
    }

    public async Task<List<Recruiter>> GetByDepartmentAsync(string department)
    {
        return await _context.Recruiters.Where(r => r.GetDepartment() == department).ToListAsync();
    }
}