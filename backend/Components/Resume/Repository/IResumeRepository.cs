using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Components.Resume.Models;

namespace backend.Components.Resume.Repository;

public interface IResumeRepository
{
    Task<backend.Components.Resume.Models.Resume> CreateAsync(backend.Components.Resume.Models.Resume resume);
    Task<IEnumerable<backend.Components.Resume.Models.Resume>> GetByApplicantIdAsync(int applicantId);
    Task DeleteAsync(int resumeId);
    Task DeleteByApplicantIdAsync(int applicantId);
}