namespace backend.Components.JobListing.Repositories.Interfaces;

using backend.Components.JobListing.Models;

public interface ISkillRepository
{
    Task<List<Skill>> GetAllSkillsListings();
    Task<Skill?> GetSkillByIdAsync(int skillId);
    Task<Skill> CreateAsync(Skill skill);
    Task<Skill> UpdateAsync(Skill skill);
    Task<bool> DeleteAsync(int skillId);
}