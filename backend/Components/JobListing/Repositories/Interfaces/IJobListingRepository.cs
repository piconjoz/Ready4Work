namespace backend.Components.JobListing.Repositories.Interfaces;

using backend.Components.JobListing.Models;

public interface IJobListingRepository
{
    Task<List<JobListing>> GetAllJobListingsAsync();
    Task<JobListing?> GetJobListingByIdAsync(int jobId);
    Task<JobListing> CreateAsync(JobListing jobLsting);
    Task<JobListing> UpdateAsync(JobListing jobListing);
    Task<bool> DeleteAsync(int jobId);
}