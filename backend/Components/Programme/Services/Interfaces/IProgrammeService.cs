namespace backend.Components.Programme.Services.Interfaces;

using backend.Components.Programme.Models;

public interface IProgrammeService
{
    Task<Programme> CreateProgrammeAsync(string programmeName);
    Task<Programme?> GetProgrammeByIdAsync(int programmeId);
    Task<Programme> UpdateProgrammeAsync(int programmeId, string programmeName);
    Task<bool> DeleteProgrammeAsync(int programmeId);
    Task<List<Programme>> GetAllProgrammesAsync();
}