namespace backend.Components.JobListing.Repositories;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Data;
using Microsoft.EntityFrameworkCore;

public class ProgrammeRepository : IProgrammeRepository
{
    private readonly ApplicationDbContext _context;

    public ProgrammeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Programme>> GetAllProgrammesAsync()
    {
        return await _context.Programmes.ToListAsync();
    }

    public async Task<Programme?> GetProgrammeByIdAsync(int programmeId)
    {
        return await _context.Programmes.FindAsync(programmeId);
    }

    public async Task<Programme> CreateAsync(Programme programme)
    {
        _context.Programmes.Add(programme);
        await _context.SaveChangesAsync();
        return programme;
    }

    public async Task<Programme> UpdateAsync(Programme programme)
    {
        _context.Entry(programme).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return programme;
    }

    public async Task<bool> DeleteAsync(int programmeId)
    {
        var programme = await _context.Programmes.FindAsync(programmeId);
        if (programme == null)
        {
            return false;
        }
        _context.Programmes.Remove(programme);
        await _context.SaveChangesAsync();
        return true;
    }
    // other related functions go here
}