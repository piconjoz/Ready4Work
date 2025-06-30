namespace backend.Components.JobListing.Repositories.Interfaces;

using backend.Components.JobListing.Models;

public interface ISkillService
{
    Task<Skill> CreateNewSkillEntryAsync(string skillName);
    Task<Skill?> GetSkillByIdAsync(int skillId);
    Task<Skill> UpdateSkillNameAsync(int skillId, string skillName);
    Task<bool> DeleteSkillByIdAsync(int skillId);
    Task<List<Skill>> GetAllSkillsAsync();
}
