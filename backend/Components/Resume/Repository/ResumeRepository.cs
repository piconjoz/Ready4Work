using backend.Data;
using backend.Components.Resume.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Components.Resume.Repository;

public class ResumeRepository : IResumeRepository
{
    private readonly ApplicationDbContext _context;
    public ResumeRepository(ApplicationDbContext context)
        => _context = context;

    public async Task<backend.Components.Resume.Models.Resume> CreateAsync(backend.Components.Resume.Models.Resume resume)
    {
        _context.Resumes.Add(resume);
        await _context.SaveChangesAsync();
        return resume;
    }

    public Task<IEnumerable<backend.Components.Resume.Models.Resume>> GetByApplicantIdAsync(int applicantId)
        => _context.Resumes
                   .Where(r => r.ApplicantId == applicantId)
                   .AsNoTracking()
                   .Cast<backend.Components.Resume.Models.Resume>()
                   .ToListAsync()
                   .ContinueWith(t => (IEnumerable<backend.Components.Resume.Models.Resume>)t.Result);

    public async Task DeleteAsync(int resumeId)
    {
        var entity = await _context.Resumes.FindAsync(resumeId);
        if (entity != null)
        {
            _context.Resumes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteByApplicantIdAsync(int applicantId)
    {
        var list = _context.Resumes.Where(r => r.ApplicantId == applicantId);
        _context.Resumes.RemoveRange(list);
        await _context.SaveChangesAsync();
    }
}