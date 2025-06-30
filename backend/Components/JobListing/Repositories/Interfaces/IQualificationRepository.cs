namespace backend.Components.JobListing.Repositories.Interfaces;

using backend.Components.JobListing.Models;

public interface IQualificationRepository
{
    Task<List<Qualification>> GetAllQualificationsAsync();
    Task<Qualification?> GetQualificationByIdAsync(int qualificationId);
    Task<Qualification> CreateAsync(Qualification qualification);
    Task<Qualification> UpdateAsync(Qualification qualification);
    Task<bool> DeleteAsync(int qualificationId);
}