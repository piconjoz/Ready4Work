namespace backend.Components.JobListing.Services;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;

    public SkillService(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    // create
    public async Task<Skill> CreateNewSkillEntryAsync(string skillName)
    {
        // check value before adding
        if (string.IsNullOrWhiteSpace(skillName)) throw new ArgumentException("Invalid skill name");

        // create new skill entry
        var skill = new Skill(skillName);
        return await _skillRepository.CreateAsync(skill);
    }

    // retrieve
    public async Task<Skill?> GetSkillByIdAsync(int skillId)
    {
        if (skillId <= 0) return null;
        return await _skillRepository.GetSkillByIdAsync(skillId);
    }

    // update
    public async Task<Skill> UpdateSkillNameAsync(int skillId, string skillName)
    {
        // check if skill exists
        var skill = await GetSkillByIdAsync(skillId);
        if (skill == null) throw new ArgumentException("Skill does not exist");

        // validate business rules if need
        if (skillId <= 0) throw new ArgumentException("Invalid Skill ID");
        if (string.IsNullOrWhiteSpace(skillName)) throw new ArgumentException("Invalid skill name");
        // More if need

        skill.SetSkill(skillName);
        return await _skillRepository.UpdateAsync(skill);
    }

    // delete
    public async Task<bool> DeleteSkillByIdAsync(int skillId)
    {
        if (skillId <= 0) return false;
        return await _skillRepository.DeleteAsync(skillId);
    }

    // retrieve all
    public async Task<List<Skill>> GetAllSkillsAsync()
    {
        return await _skillRepository.GetAllSkillsAsync();
    }

    // Other business logics handle here
}
