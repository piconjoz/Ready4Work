namespace backend.Components.Application.Repository;

using backend.Components.Application.Models;

public interface IApplicationRepository
{
    Task<JobApplication?> GetByIdAsync(int applicationId);
    Task<JobApplication?> GetByApplicantAndJobAsync(int applicantId, int jobId);
    Task<List<JobApplication>> GetByApplicantIdAsync(int applicantId);
    Task<JobApplication> CreateAsync(JobApplication application);
    Task<JobApplication> UpdateAsync(JobApplication application);
    Task<bool> DeleteAsync(int applicationId);
    Task<List<JobApplication>> GetAllApplicationsAsync(List<int> jobIds);
}