namespace backend.Components.JobListing.Repositories;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Data;

public class RemunerationTypeRepository : IRemunerationTypeRepository
{
    private readonly ApplicationDbContext _context;

    public RemunerationTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RemunerationType?> GetByIdAsync(int remunerationId)
    {
        return await _context.RemunerationTypes.FirstOrDefaultAsync(r => EF.Property<int>(r, "RemunerationId") == remunerationId);
    }

    public async Task<List<RemunerationType>> GetAllAsync()
    {
        return await _context.RemunerationTypes.ToListAsync();
    }

    public async Task<RemunerationType> CreateAsync(RemunerationType remunerationType)
    {
        _context.RemunerationTypes.Add(remunerationType);
        await _context.SaveChangesAsync();
        return remunerationType;
    }

    public async Task<RemunerationType> UpdateAsync(RemunerationType remunerationType)
    {
        _context.RemunerationTypes.Update(remunerationType);
        await _context.SaveChangesAsync();
        return remunerationType;
    }

    public async Task<bool> DeleteAsync(int remunerationId)
    {
        var remunerationType = await GetByIdAsync(remunerationId);
        if (remunerationType == null) return false;

        _context.RemunerationTypes.Remove(remunerationType);
        await _context.SaveChangesAsync();
        return true;
    }
}