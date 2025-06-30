namespace backend.Components.JobListing.Services.Interfaces;

using backend.Components.JobListing.Models;

public interface IQualificationService
{
    Task<Qualification> CreateNewQualificationAsync(int jobId, int programmeId);
    Task<Qualification?> GetQualificationByIdAsync(int qualificationId);
    Task<Qualification> UpdateQualifcationAsync(int qualificationId, int programmeId, int jobId);
    Task<bool> DeleteQualificationAsync(int qualificationId);
    Task<List<Qualification>> GetAllQualificationAsync();
}