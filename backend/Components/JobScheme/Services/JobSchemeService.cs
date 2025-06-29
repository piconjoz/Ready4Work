namespace backend.JobScheme.Services;

using backend.JobScheme.Models;
using backend.JobScheme.DTOs;
using backend.JobScheme.Services.Interfaces;
using backend.JobScheme.Repositories.Interfaces;

public class JobSchemeService : IJobSchemeService
{
    private readonly IJobSchemeRepository _jobSchemeRepository;
    
    public JobSchemeService(IJobSchemeRepository jobSchemeRepository)
    {
        _jobSchemeRepository = jobSchemeRepository;
    }
    
    public async Task<JobScheme> CreateJobSchemeAsync(string schemeName)
    {
        if (string.IsNullOrWhiteSpace(schemeName))
            throw new ArgumentException("Job scheme name cannot be empty");
            
        // Check if name already exists
        var existingSchemes = await GetAllJobSchemesAsync();
        if (existingSchemes.Any(s => s.GetSchemeName().Equals(schemeName, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("This job scheme already exists");
            
        var jobScheme = new JobScheme(schemeName);
        
        return await _jobSchemeRepository.CreateAsync(jobScheme);
    }
    
    public async Task<JobScheme?> GetJobSchemeByIdAsync(int schemeId)
    {
        if (schemeId <= 0) return null;

        return await _jobSchemeRepository.GetByIdAsync(schemeId);
    }
    
    public async Task<JobScheme> UpdateJobSchemeAsync(int schemeId, string schemeName)
    {
        var jobScheme = await GetJobSchemeByIdAsync(schemeId);
        if (jobScheme == null) 
            throw new ArgumentException("Job scheme does not exist");
        
        if (string.IsNullOrWhiteSpace(schemeName))
            throw new ArgumentException("Job scheme name cannot be empty");
            
        // Check if updated name would create a duplicate
        var existingSchemes = await GetAllJobSchemesAsync();
        if (existingSchemes.Any(s => s.GetSchemeName().Equals(schemeName, StringComparison.OrdinalIgnoreCase) && 
                                     s.GetSchemeId() != schemeId))
            throw new InvalidOperationException("Another job scheme with this name already exists");
        
        jobScheme.SetSchemeName(schemeName);
        
        return await _jobSchemeRepository.UpdateAsync(jobScheme);
    }
    
    public async Task<bool> DeleteJobSchemeAsync(int schemeId)
    {
        if (schemeId <= 0) return false;

        return await _jobSchemeRepository.DeleteAsync(schemeId);
    }
    
    public async Task<List<JobScheme>> GetAllJobSchemesAsync()
    {
        return await _jobSchemeRepository.GetAllAsync();
    }
    
    public JobSchemeResponseDTO ConvertToResponseDTO(JobScheme jobScheme)
    {
        return new JobSchemeResponseDTO
        {
            SchemeId = jobScheme.GetSchemeId(),
            SchemeName = jobScheme.GetSchemeName()
        };
    }
    
    public async Task<List<JobSchemeResponseDTO>> GetAllJobSchemeDTOsAsync()
    {
        var jobSchemes = await GetAllJobSchemesAsync();
        return jobSchemes.Select(ConvertToResponseDTO).ToList();
    }
}