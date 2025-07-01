namespace backend.Components.Programme.Repositories.Interfaces;

using backend.Components.Programme.Models;
public interface IProgrammeRepository
{
    Task<List<Programme>> GetAllProgrammesAsync();
    Task<Programme?> GetProgrammeByIdAsync(int programmeId);
    Task<Programme> CreateAsync(Programme programme);
    Task<Programme> UpdateAsync(Programme programme);
    Task<bool> DeleteAsync(int programmeId);
}