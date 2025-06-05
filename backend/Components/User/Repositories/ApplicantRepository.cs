namespace backend.User.Repositories;

using backend.User.Models;
using backend.User.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ApplicantRepository : IApplicantRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Applicant?> GetByIdAsync(int applicantId)
    {
        return await _context.Applicants.FirstOrDefaultAsync(a => a.GetApplicantId() == applicantId);
    }

    public async Task<Applicant?> GetByUserIdAsync(int userId)
    {
        return await _context.Applicants.FirstOrDefaultAsync(a => a.GetUserId() == userId);
    }

    public async Task<Applicant> CreateAsync(Applicant applicant)
    {
        _context.Applicants.Add(applicant);
        await _context.SaveChangesAsync();
        return applicant;
    }

    public async Task<Applicant> UpdateAsync(Applicant applicant)
    {
        applicant.SetUpdatedAt(DateTime.UtcNow);
        _context.Applicants.Update(applicant);
        await _context.SaveChangesAsync();
        return applicant;
    }

    public async Task<bool> DeleteAsync(int applicantId)
    {
        var applicant = await GetByIdAsync(applicantId);
        if (applicant == null) return false;

        _context.Applicants.Remove(applicant);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByUserIdAsync(int userId)
    {
        return await _context.Applicants.AnyAsync(a => a.GetUserId() == userId);
    }

    public async Task<List<Applicant>> GetByProgrammeIdAsync(int programmeId)
    {
        return await _context.Applicants.Where(a => a.GetProgrammeId() == programmeId).ToListAsync();
    }

    public async Task<List<Applicant>> GetByAdmitYearAsync(int admitYear)
    {
        return await _context.Applicants.Where(a => a.GetAdmitYear() == admitYear).ToListAsync();
    }
}