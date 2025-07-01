namespace backend.Components.JobSkill.Repositories.Interfaces;

using backend.Components.JobSkill.Models;

public interface IJobSkillRepository
{
    Task<List<JobSkill>> GetAllJobSkillsAsync();
    Task<JobSkill?> GetJobSkillsByIdAsync(int jobSkillId);
    Task<JobSkill> CreateAsync(JobSkill jobSkill);
    Task<JobSkill> UpdateAsync(JobSkill jobSkill);
    Task<bool> DeleteAsync(int jobSkillId);
}