namespace backend.User.Repositories;

using backend.User.Models;
using backend.User.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Data;

public class ApplicantRepository : IApplicantRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Applicant?> GetByIdAsync(int applicantId)
    {
        // use ef.property instead of a.GetApplicantId()
        return await _context.Applicants.FirstOrDefaultAsync(a => EF.Property<int>(a, "ApplicantId") == applicantId);
    }

    public async Task<Applicant?> GetByUserIdAsync(int userId)
    {
        // use ef.property instead of a.GetUserId()
        return await _context.Applicants.FirstOrDefaultAsync(a => EF.Property<int>(a, "UserId") == userId);
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
        return await _context.Applicants.AnyAsync(a => EF.Property<int>(a, "UserId") == userId);
    }

    public async Task<List<Applicant>> GetByProgrammeIdAsync(int programmeId)
    {
        return await _context.Applicants.Where(a => EF.Property<int>(a, "ProgrammeId") == programmeId).ToListAsync();
    }

    public async Task<List<Applicant>> GetByAdmitYearAsync(int admitYear)
    {
        return await _context.Applicants.Where(a => EF.Property<int>(a, "AdmitYear") == admitYear).ToListAsync();
    }
}