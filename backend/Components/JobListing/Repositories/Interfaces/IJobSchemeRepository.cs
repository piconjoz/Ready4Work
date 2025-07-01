namespace backend.Components.JobListing.Repositories.Interfaces;

using backend.Components.JobListing.Models;

public interface IJobSchemeRepository
{
    Task<JobScheme?> GetByIdAsync(int schemeId);
    Task<List<JobScheme>> GetAllAsync();
    Task<JobScheme> CreateAsync(JobScheme jobScheme);
    Task<JobScheme> UpdateAsync(JobScheme jobScheme);
    Task<bool> DeleteAsync(int schemeId);
}