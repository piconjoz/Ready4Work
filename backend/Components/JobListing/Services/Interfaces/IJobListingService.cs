namespace backend.Components.JobListing.Services.Interfaces;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.DTOs;

public interface IJobListingService
{
    // CRUD
    Task<JobListing> CreateJobListingAsync(int recruiterId, string jobRequirements, string jobDesription, string listingName, DateTime deadline, int maxVacancies, bool isVisble, string renumerationType, string jobDuration, float rate, string workingHours, string jobScheme, int permittedQualifications, int skillsets, string jobStatus);
    Task<JobListing?> GetJobListingByIdAsync(int jobId);
    Task<JobListing> UpdateJobListingDetailsAync(int jobId, string jobRequirements, string jobDesription, string listingName, DateTime deadline, int maxVacancies, bool isVisble, string renumerationType, string jobDuration, float rate, string workingHours, string jobScheme, int permittedQualifications, int skillsets, string jobStatus);
    Task<bool> DeleteJobListingAsync(int jobId);
    Task<List<JobListing>> GetAllJobListingAsync();
    // Other implemenation go here
    Task<List<JobListingRecruiterShowAllDTO>> GetAllRecruiterJobListingsAsync(int recruiterId);
}