namespace backend.Components.JobListing.Services;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.DTOs;
using backend.Components.JobListing.Services.Interfaces;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Components.Application.Repository;
using backend.User.Repositories.Interfaces;

public class JobListingService : IJobListingService
{
    private readonly IJobListingRepository _jobListingRepository;
    private readonly IRecruiterRepository _recruiterRepository;
    private readonly IApplicationRepository _applicationRepository;

    public JobListingService(IJobListingRepository jobListingRepository, IRecruiterRepository recruiterRepository, IApplicationRepository applicationRepository)
    {
        _jobListingRepository = jobListingRepository;
        _recruiterRepository = recruiterRepository;
        _applicationRepository = applicationRepository;
    }

    // create
    public async Task<JobListing> CreateJobListingAsync(int recruiterId, string jobRequirements, string jobDesription, string listingName, DateTime deadline, int maxVacancies, bool isVisble, string remunerationType, string jobDuration, float rate, string workingHours, string jobScheme, int permittedQualifications, int skillsets, string jobStatus)
    {
        // validate recruiter exists
        // var recruiter = await _recruiterService.GetRecruiterByIdAsync(recruiterId);
        var recruiter = await _recruiterRepository.GetByIdAsync(recruiterId);
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
    // Retrieve all job listings under recruiter
    public async Task<List<JobListingResponseDTO>> GetAllRecruiterJobListingsAsync(int recruiterId)
    {
        /* 
        retrieve how many job listings are under recruiter, check if visible
        retrieve how many applications are under each listings
        calculate how available space left(successful/rejected/pending) for each listing,
        calculate days left  
        place in DTO relevant details and send back to controller
        */

        // get all relevant job listings for recruiter
        var jobListings = await _jobListingRepository.GetVisibleJobListingsByRecruiterIdAsync(recruiterId);
        if (!jobListings.Any())
        {
            // return empty list
            return new List<JobListingResponseDTO>();
        }
        // get all applications that are available based on the ids of the list of job Listings
        var ids = jobListings.Select(j => j.GetJobId()).ToList();
        var applications = await _applicationRepository.GetAllApplicationsAsync(ids);
        // for each listing calculate accordingly
        var listingPageDTO = jobListings.Select(j =>
        {
            var listingApps = applications.Where(a => a.JobId == j.GetJobId()).ToList();
            var daysRemaining = Math.Max(0, (j.GetDeadline() - DateTime.UtcNow).Days);
            var visibility = j.GetIsVisible() ? "Public" : "Private";
            int pending = listingApps.Count(a => a.Status == "pending");
            int accepted = listingApps.Count(a => a.Status == "accepted");

            return new JobListingResponseDTO
            {
                ListingName = j.GetListingName(),
                PublishedDate = j.GetCreatedAt(),
                DaysRemaining = daysRemaining,
                Visibility = visibility,
                Pending = pending,
                Approved = accepted,
                maxVacancies = j.GetMaxVacancies()
            };
        }).ToList();

        return listingPageDTO;
    }

    // method for querying expiring job listings
    // method for checking expired job listings, if expired set to not visible
}