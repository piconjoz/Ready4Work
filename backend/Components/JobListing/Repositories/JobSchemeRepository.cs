namespace backend.Components.JobListing.Repositories;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Data;

public class JobSchemeRepository : IJobSchemeRepository
{
    private readonly ApplicationDbContext _context;

    public JobSchemeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JobScheme?> GetByIdAsync(int schemeId)
    {
        return await _context.JobSchemes.FirstOrDefaultAsync(j => EF.Property<int>(j, "SchemeId") == schemeId);
    }

    public async Task<List<JobScheme>> GetAllAsync()
    {
        return await _context.JobSchemes.ToListAsync();
    }

    public async Task<JobScheme> CreateAsync(JobScheme jobScheme)
    {
        _context.JobSchemes.Add(jobScheme);
        await _context.SaveChangesAsync();
        return jobScheme;
    }

    public async Task<JobScheme> UpdateAsync(JobScheme jobScheme)
    {
        _context.JobSchemes.Update(jobScheme);
        await _context.SaveChangesAsync();
        return jobScheme;
    }

    public async Task<bool> DeleteAsync(int schemeId)
    {
        var jobScheme = await GetByIdAsync(schemeId);
        if (jobScheme == null) return false;

        _context.JobSchemes.Remove(jobScheme);
        await _context.SaveChangesAsync();
        return true;
    }
}