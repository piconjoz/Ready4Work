namespace backend.Components.JobListing.Repositories;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Data;
using Microsoft.EntityFrameworkCore;

public class QualificationRepository : IQualificationRepository
{
    private readonly ApplicationDbContext _context;

    public QualificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Qualification>> GetAllQualificationListings()
    {
        return await _context.Qualifications.ToListAsync();
    }

    public async Task<Qualification?> GetQualificationByIdAsync(int qualificationId)
    {
        return await _context.Qualifications.FindAsync(qualificationId);
    }

    public async Task<Qualification> CreateAsync(Qualification qualification)
    {
        _context.Qualifications.Add(qualification);
        await _context.SaveChangesAsync();
        return qualification;
    }

    public async Task<Qualification> UpdateAsync(Qualification qualification)
    {
        _context.Entry(qualification).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return qualification;
    }

    public async Task<bool> DeleteAsync(int qualificationId)
    {
        var qualification = await _context.Qualifications.FindAsync(qualificationId);
        if (qualification == null)
        {
            return false;
        }
        _context.Qualifications.Remove(qualification);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<List<Qualification>> GetAllQualificationListingsAsync()
    {
        throw new NotImplementedException();
    }

    // other related functions go here
}