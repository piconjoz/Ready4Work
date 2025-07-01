namespace backend.Components.Application.Repository;

using backend.Components.Application.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JobApplication?> GetByIdAsync(int applicationId)
    {
        return await _context.JobApplications.FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
    }

    public async Task<JobApplication?> GetByApplicantAndJobAsync(int applicantId, int jobListingId)
    {
        return await _context.JobApplications.FirstOrDefaultAsync(a => 
            a.ApplicantId == applicantId && a.JobListingId == jobListingId);
    }

    public async Task<List<JobApplication>> GetByApplicantIdAsync(int applicantId)
    {
        return await _context.JobApplications
            .Where(a => a.ApplicantId == applicantId)
            .ToListAsync();
    }

    public async Task<JobApplication> CreateAsync(JobApplication application)
    {
        _context.JobApplications.Add(application);
        await _context.SaveChangesAsync();
        return application;
    }

    public async Task<JobApplication> UpdateAsync(JobApplication application)
    {
        application.UpdatedAt = DateTime.UtcNow;
        _context.JobApplications.Update(application);
        await _context.SaveChangesAsync();
        return application;
    }

    public async Task<bool> DeleteAsync(int applicationId)
    {
        var application = await GetByIdAsync(applicationId);
        if (application == null) return false;

        _context.JobApplications.Remove(application);
        await _context.SaveChangesAsync();
        return true;
    }
}