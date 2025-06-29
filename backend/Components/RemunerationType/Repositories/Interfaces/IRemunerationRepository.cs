namespace backend.RemunerationType.Repositories.Interfaces;

using backend.RemunerationType.Models;

public interface IRemunerationTypeRepository
{
    Task<RemunerationType?> GetByIdAsync(int remunerationId);
    Task<List<RemunerationType>> GetAllAsync();
    Task<RemunerationType> CreateAsync(RemunerationType remunerationType);
    Task<RemunerationType> UpdateAsync(RemunerationType remunerationType);
    Task<bool> DeleteAsync(int remunerationId);
}