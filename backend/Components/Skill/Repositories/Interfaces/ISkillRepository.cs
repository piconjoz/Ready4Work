namespace backend.Components.Skill.Repositories.Interfaces;

using backend.Components.Skill.Models;

public interface ISkillRepository
{
    Task<List<Skill>> GetAllSkillsAsync();
    Task<Skill?> GetSkillByIdAsync(int skillId);
    Task<Skill> CreateAsync(Skill skill);
    Task<Skill> UpdateAsync(Skill skill);
    Task<bool> DeleteAsync(int skillId);
}