namespace backend.Components.JobListing.Repositories.Interfaces;

using backend.Components.JobListing.Models;

public interface IRemunerationTypeRepository
{
    Task<RemunerationType?> GetByIdAsync(int remunerationId);
    Task<List<RemunerationType>> GetAllAsync();
    Task<RemunerationType> CreateAsync(RemunerationType remunerationType);
    Task<RemunerationType> UpdateAsync(RemunerationType remunerationType);
    Task<bool> DeleteAsync(int remunerationId);
}