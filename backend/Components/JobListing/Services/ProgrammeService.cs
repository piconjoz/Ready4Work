namespace backend.Components.JobListing.Services;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Components.JobListing.Services.Interfaces;

public class ProgrammeService : IProgrammeService
{
    private readonly IProgrammeRepository _programmeRepository;
    public ProgrammeService(IProgrammeRepository programmeRepository)
    {
        _programmeRepository = programmeRepository;
    }

    // create
    public async Task<Programme> CreateProgrammeAsync(string programmeName)
    {
        // business checks
        if (string.IsNullOrWhiteSpace(programmeName)) throw new ArgumentException("Invalid programme name");
        var programme = new Programme(programmeName);
        return await _programmeRepository.CreateAsync(programme);
    }

    // retrieve
    public async Task<Programme?> GetProgrammeByIdAsync(int programmeId)
    {
        // business checks/validation
        if (programmeId <= 0) throw new ArgumentException("Invalid programme ID");
        return await _programmeRepository.GetProgrammeByIdAsync(programmeId);
    }

    // update
    public async Task<Programme> UpdateProgrammeAsync(int programmeId, string programmeName)
    {
        // validate/check
        var programme = await GetProgrammeByIdAsync(programmeId);
        if (programme == null) throw new ArgumentException("Programme does not exists");

        if (programmeId <= 0) throw new ArgumentException("Invalid Programme ID");
        if (string.IsNullOrWhiteSpace(programmeName)) throw new ArgumentException("Invalid Programme name");

        programme.SetProgrammeName(programmeName);
        return await _programmeRepository.UpdateAsync(programme);
    }

    // delete
    public async Task<bool> DeleteProgrammeAsync(int programmeId)
    {
        // validate
        if (programmeId <= 0) return false;
        return await _programmeRepository.DeleteAsync(programmeId);
    }

    // retrieve all
    public async Task<List<Programme>> GetAllProgrammesAsync()
    {
        return await _programmeRepository.GetAllProgrammesAsync();
    }
}