namespace backend.Components.JobListing.Services.Interfaces;

using backend.Components.JobListing.Models;

public interface IRemunerationTypeService
{
    Task<RemunerationType> CreateRemunerationTypeAsync(string type);
    Task<RemunerationType?> GetRemunerationTypeByIdAsync(int remunerationId);
    Task<RemunerationType> UpdateRemunerationTypeAsync(int remunerationId, string type);
    Task<bool> DeleteRemunerationTypeAsync(int remunerationId);
    Task<List<RemunerationType>> GetAllRemunerationTypesAsync();
}