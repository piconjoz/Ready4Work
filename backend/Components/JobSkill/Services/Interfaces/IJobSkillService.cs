namespace backend.Components.JobSkill.Services.Interfaces;

using backend.Components.JobSkill.Models;

public interface IJobSkillService
{
    Task<JobSkill> CreateJobSkillAsync(int skill_id, int job_id);
    Task<JobSkill?> GetJobSkillByIdAsync(int jobSkillId);
    Task<JobSkill> UpdateJobSkillAsync(int jobSkillId);
    Task<bool> DeleteJobSkillAsync(int jobSkillId);
    Task<List<JobSkill>> GetAllJobSkillAsync();

}