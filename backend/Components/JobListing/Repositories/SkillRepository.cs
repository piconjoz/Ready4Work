namespace backend.Components.JobListing.Repositories;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Data;
using Microsoft.EntityFrameworkCore;

public class SkillRepository : ISkillRepository
{
    private readonly ApplicationDbContext _context;

    public SkillRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Skill>> GetAllSkillsListings()
    {
        return await _context.Skills.ToListAsync();
    }

    public async Task<Skill?> GetSkillByIdAsync(int skillId)
    {
        return await _context.Skills.FindAsync(skillId);
    }

    public async Task<Skill> CreateAsync(Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return skill;
    }

    public async Task<Skill> UpdateAsync(Skill skill)
    {
        _context.Entry(skill).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return skill;
    }

    public async Task<bool> DeleteAsync(int skillId)
    {
        var skill = await _context.Skills.FindAsync(skillId);
        if (skill == null)
        {
            return false;
        }
        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<List<Skill>> GetAllJobListingsAsync()
    {
        throw new NotImplementedException();
    }

    // other related functions go here
}