namespace backend.Components.JobListing.Services;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Components.JobListing.Services.Interfaces;

public class JobSkillService : IJobSkillService
{
    private readonly IJobSkillRepository _jobSkillRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IJobListingRepository _jobListingRepository;

    public JobSkillService(IJobSkillRepository jobSkillRepository, IJobListingRepository jobListingRepository, ISkillRepository skillRepository)
    {
        _jobSkillRepository = jobSkillRepository;
        _jobListingRepository = jobListingRepository;
        _skillRepository = skillRepository;
    }

    //create
    public async Task<JobSkill> CreateJobSkillAsync(int skill_id, int job_id)
    {
        // validate id for skill and job
        var skill = await _skillRepository.GetSkillByIdAsync(skill_id);
        if (skill == null) throw new ArgumentException("Skill does not exists");

        var job = await _jobListingRepository.GetJobListingByIdAsync(job_id);
        if (job == null) throw new ArgumentException("Job does not exists");

        var jobSkill = new JobSkill(skill_id, job_id);
        return await _jobSkillRepository.CreateAsync(jobSkill);
    }

    //retrieve
    public async Task<JobSkill?> GetJobSkillByIdAsync(int jobSkillId)
    {
        if (jobSkillId <= 0) return null;
        return await _jobSkillRepository.GetJobSkillsByIdAsync(jobSkillId);
    }

    //update
    // HIGHLY LIKELY WILL NOT NEED THIS
    public async Task<JobSkill> UpdateJobSkillAsync(int jobSkillId)
    {
        // check
        var jobSkill = await GetJobSkillByIdAsync(jobSkillId);
        if (jobSkill == null) throw new ArgumentException("Invalid Job Skill does not exist");

        if (jobSkillId <= 0) throw new ArgumentException("Invalid Job Skill ID");

        jobSkill.SetJobSkillId(jobSkillId);
        return await _jobSkillRepository.UpdateAsync(jobSkill);
    }

    //delete
    public async Task<bool> DeleteJobSkillAsync(int jobSkillId)
    {
        if (jobSkillId <= 0) return false;
        return await _jobSkillRepository.DeleteAsync(jobSkillId);
    }

    //retrieve all
    public async Task<List<JobSkill>> GetAllJobSkillAsync()
    {
        return await _jobSkillRepository.GetAllJobSkillsAsync();
    }
}