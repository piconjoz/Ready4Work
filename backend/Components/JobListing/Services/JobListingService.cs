namespace backend.Components.JobListing.Services;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Services.Interfaces;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.User.Services.Interfaces;

public class JobListingService : IJobListingService
{
    private readonly IJobListingRepository _jobListingRepository;
    private readonly IRecruiterService _recruiterService;

    public JobListingService(IJobListingRepository jobListingRepository, IRecruiterService recruiterService)
    {
        _jobListingRepository = jobListingRepository;
        _recruiterService = recruiterService;
    }

    // create
    public async Task<JobListing> CreateJobListingAsync(int recruiterId, string jobRequirements, string jobDesription, string listingName, DateTime deadline, int maxVacancies, bool isVisble, string remunerationType, string jobDuration, float rate, string workingHours, string jobScheme, int permittedQualifications, int skillsets, string jobStatus)
    {
        // validate recruiter exists
        var recruiter = await _recruiterService.GetRecruiterByIdAsync(recruiterId);
        if (recruiter == null) throw new ArgumentException("Recruiter does not exist");

        // validate business rules
        if (recruiterId <= 0) throw new ArgumentException("Invalid Recruiter ID");
        // MORE

        var jobListing = new JobListing(recruiterId, jobRequirements, jobDesription, listingName, deadline, maxVacancies, isVisble, remunerationType, jobDuration, rate, workingHours, jobScheme, permittedQualifications, skillsets, jobStatus);
        return await _jobListingRepository.CreateAsync(jobListing);
    }

    // retrieve
    public async Task<JobListing?> GetJobListingByIdAsync(int jobId)
    {
        // check if id is valild
        if (jobId <= 0) return null;
        return await _jobListingRepository.GetJobListingByIdAsync(jobId);
    }

    // update
    public async Task<JobListing> UpdateJobListingDetailsAync(int jobId, string jobRequirements, string jobDesription, string listingName, DateTime deadline, int maxVacancies, bool isVisble, string remunerationType, string jobDuration, float rate, string workingHours, string jobScheme, int permittedQualifications, int skillsets, string jobStatus)
    {
        var jobListing = await GetJobListingByIdAsync(jobId);
        if (jobListing == null) throw new ArgumentException("Job Listing does not exist");

        // validate business rules
        if (jobId <= 0) throw new ArgumentException("Invalid Job Listing ID");
        // MORE

        // update fields if pass validation
        jobListing.SetJobRequirements(jobRequirements);
        jobListing.SetJobDescription(jobDesription);
        jobListing.SetListingName(listingName);
        jobListing.SetDeadline(deadline);
        jobListing.SetMaxVacancies(maxVacancies);
        jobListing.SetIsVisible(isVisble);
        jobListing.SetRemunerationType(remunerationType);
        jobListing.SetJobDuration(jobDuration);
        jobListing.SetRate(rate);
        jobListing.SetWorkingHours(workingHours);
        jobListing.SetPermittedQualifications(permittedQualifications);
        jobListing.SetSkillsets(skillsets);
        jobListing.SetUpdatedAt(DateTime.UtcNow);

        return await _jobListingRepository.UpdateAsync(jobListing);

    }

    // delete
    public async Task<bool> DeleteJobListingAsync(int jobId)
    {
        if (jobId <= 0) return false;
        return await _jobListingRepository.DeleteAsync(jobId);
    }

    // retrieve all
    public async Task<List<JobListing>> GetAllJobListingAsync()
    {
        return await _jobListingRepository.GetAllJobListingsAsync();
    }

    // Other business logics handle here
}