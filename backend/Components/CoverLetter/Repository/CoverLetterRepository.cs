namespace backend.Components.CoverLetter.Repository;

using backend.Components.CoverLetter.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data;

public class CoverLetterRepository : ICoverLetterRepository
{
    private readonly ApplicationDbContext _context;

    public CoverLetterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CoverLetter?> GetByIdAsync(int coverLetterId)
    {
        return await _context.CoverLetters.FirstOrDefaultAsync(c => c.CoverLetterId == coverLetterId);
    }

    public async Task<List<CoverLetter>> GetByApplicantIdAsync(int applicantId)
    {
        return await _context.CoverLetters
            .Where(c => c.ApplicantId == applicantId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<CoverLetter> CreateAsync(CoverLetter coverLetter)
    {
        _context.CoverLetters.Add(coverLetter);
        await _context.SaveChangesAsync();
        return coverLetter;
    }

    public async Task<CoverLetter> UpdateAsync(CoverLetter coverLetter)
    {
        coverLetter.UpdatedAt = DateTime.UtcNow;
        _context.CoverLetters.Update(coverLetter);
        await _context.SaveChangesAsync();
        return coverLetter;
    }

    public async Task<bool> DeleteAsync(int coverLetterId)
    {
        var coverLetter = await GetByIdAsync(coverLetterId);
        if (coverLetter == null) return false;

        // Delete the physical file
        if (File.Exists(coverLetter.CoverLetterPath))
        {
            File.Delete(coverLetter.CoverLetterPath);
        }

        _context.CoverLetters.Remove(coverLetter);
        await _context.SaveChangesAsync();
        return true;
    }
}