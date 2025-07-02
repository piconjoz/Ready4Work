namespace backend.Components.JobScheme.Services.Interfaces;

using backend.Components.JobScheme.Models;
// using backend.Components.JobListing.DTOs;

public interface IJobSchemeService
{
    Task<JobScheme> CreateJobSchemeAsync(string schemeName);
    Task<JobScheme?> GetJobSchemeByIdAsync(int schemeId);
    Task<JobScheme> UpdateJobSchemeAsync(int schemeId, string schemeName);
    Task<bool> DeleteJobSchemeAsync(int schemeId);
    Task<List<JobScheme>> GetAllJobSchemesAsync();
    // JobSchemeResponseDTO ConvertToResponseDTO(JobScheme jobScheme);
    // Task<List<JobSchemeResponseDTO>> GetAllJobSchemeDTOsAsync();
}