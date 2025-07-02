namespace backend.Components.JobListing.Repositories;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Data;
using Microsoft.EntityFrameworkCore;

public class JobListingRepository : IJobListingRepository
{
    private readonly ApplicationDbContext _context;

    public JobListingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobListing>> GetAllJobListingsAsync()
    {
        return await _context.JobListings.ToListAsync();
    }

    public async Task<JobListing?> GetJobListingByIdAsync(int jobId)
    {
        return await _context.JobListings.FindAsync(jobId);
    }

    public async Task<JobListing> CreateAsync(JobListing jobLsting)
    {
        _context.JobListings.Add(jobLsting);
        await _context.SaveChangesAsync();
        return jobLsting;
    }

    public async Task<JobListing> UpdateAsync(JobListing jobListing)
    {
        _context.Entry(jobListing).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return jobListing;
    }

    public async Task<bool> DeleteAsync(int jobId)
    {
        var jobListing = await _context.JobListings.FindAsync(jobId);
        if (jobListing == null)
        {
            return false;
        }
        _context.JobListings.Remove(jobListing);
        await _context.SaveChangesAsync();
        return true;
    }
    // other related functions go here
}