namespace backend.Components.JobListing.Services;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Components.JobListing.Services.Interfaces;

public class QualificationService : IQualificationService
{
    private readonly IQualificationRepository _qualificationRepository;
    private readonly IJobListingRepository _jobListingRepository;
    private readonly IProgrammeRepository _programmeRepository;
    public QualificationService(IQualificationRepository qualificationRepository, IJobListingRepository jobListingRepository, IProgrammeRepository programmeRepository)
    {
        _qualificationRepository = qualificationRepository;
        _jobListingRepository = jobListingRepository;
        _programmeRepository = programmeRepository;
    }

    //create
    public async Task<Qualification> CreateNewQualificationAsync(int jobId, int programmeId)
    {
        // check/validate existing job and programmeId
        var jobListing = await _jobListingRepository.GetJobListingByIdAsync(jobId);
        if (jobListing == null) throw new ArgumentException("Job does not exist");

        var programme = await _programmeRepository.GetProgrammeByIdAsync(jobId);
        if (programme == null) throw new ArgumentException("Programme does not exist");

        var qualification = new Qualification(jobId, programmeId);
        return await _qualificationRepository.CreateAsync(qualification);
    }

    // retrieve
    public async Task<Qualification?> GetQualificationByIdAsync(int qualificationId)
    {
        // business checks/validation
        if (qualificationId <= 0) throw new ArgumentException("Invalid qualification ID");
        return await _qualificationRepository.GetQualificationByIdAsync(qualificationId);
    }

    // update
    // HIGHLY LIKELY NO NEED
    public async Task<Qualification> UpdateQualifcationAsync(int qualificationId, int programmeId, int jobId)
    {
        // validate/check
        var qualification = await GetQualificationByIdAsync(qualificationId);
        if (qualification == null) throw new ArgumentException("Qualification does not exist");
        var job = await _jobListingRepository.GetJobListingByIdAsync(jobId);
        if (job == null) throw new ArgumentException("Job does not exists");
        var programme = await _programmeRepository.GetProgrammeByIdAsync(programmeId);
        if (programme == null) throw new ArgumentException("Programme does not exists");

        // business checks
        if (programmeId <= 0 || jobId <= 0) throw new ArgumentException("Invalid ID");

        qualification.SetQualificationId(qualificationId);
        return await _qualificationRepository.UpdateAsync(qualification);
    }

    // delete
    public async Task<bool> DeleteQualificationAsync(int qualificationId)
    {
        // validate
        if (qualificationId <= 0) return false;
        return await _qualificationRepository.DeleteAsync(qualificationId);
    }

    // retrieve all
    public async Task<List<Qualification>> GetAllQualificationAsync()
    {
        return await _qualificationRepository.GetAllQualificationsAsync();
    }
}