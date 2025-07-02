namespace backend.Components.Skill.Repositories.Interfaces;

using backend.Components.Skill.Models;

public interface ISkillService
{
    Task<Skill> CreateNewSkillEntryAsync(string skillName);
    Task<Skill?> GetSkillByIdAsync(int skillId);
    Task<Skill> UpdateSkillNameAsync(int skillId, string skillName);
    Task<bool> DeleteSkillByIdAsync(int skillId);
    Task<List<Skill>> GetAllSkillsAsync();
}
