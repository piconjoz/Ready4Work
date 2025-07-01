namespace backend.Components.JobListing.Repositories;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Data;
using Microsoft.EntityFrameworkCore;

public class JobSkillRepository : IJobSkillRepository
{
    private readonly ApplicationDbContext _context;

    public JobSkillRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobSkill>> GetAllJobSkillsAsync()
    {
        return await _context.JobSkills.ToListAsync();
    }

    public async Task<JobSkill?> GetJobSkillsByIdAsync(int jobSkillId)
    {
        return await _context.JobSkills.FindAsync(jobSkillId);
    }

    public async Task<JobSkill> CreateAsync(JobSkill jobSkill)
    {
        _context.JobSkills.Add(jobSkill);
        await _context.SaveChangesAsync();
        return jobSkill;
    }

    public async Task<JobSkill> UpdateAsync(JobSkill jobSkill)
    {
        _context.Entry(jobSkill).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return jobSkill;
    }

    public async Task<bool> DeleteAsync(int jobSkillId)
    {
        var jobSkill = await _context.Skills.FindAsync(jobSkillId);
        if (jobSkill == null)
        {
            return false;
        }
        _context.Skills.Remove(jobSkill);
        await _context.SaveChangesAsync();
        return true;
    }
    // other related functions go here
}