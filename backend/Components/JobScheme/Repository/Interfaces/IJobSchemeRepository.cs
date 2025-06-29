namespace backend.JobScheme.Repositories.Interfaces;

using backend.JobScheme.Models;

public interface IJobSchemeRepository
{
    Task<JobScheme?> GetByIdAsync(int schemeId);
    Task<List<JobScheme>> GetAllAsync();
    Task<JobScheme> CreateAsync(JobScheme jobScheme);
    Task<JobScheme> UpdateAsync(JobScheme jobScheme);
    Task<bool> DeleteAsync(int schemeId);
}