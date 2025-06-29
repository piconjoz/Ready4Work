namespace backend.Components.JobListing.Repositories.Interfaces;

using backend.Components.JobListing.Models;
public interface IProgrammeRepository
{
    Task<List<Programme>> GetAllProgrammesListings();
    Task<Programme?> GetProgrammeByIdAsync(int programmeId);
    Task<Programme> CreateAsync(Programme programme);
    Task<Programme> UpdateAsync(Programme programme);
    Task<bool> DeleteAsync(int programmeId);
}