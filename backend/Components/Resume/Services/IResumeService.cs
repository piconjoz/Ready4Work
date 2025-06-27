using backend.Components.Resume.DTOs;

namespace backend.Components.Resume.Services;

public interface IResumeService
{
    Task<ResumeResponseDto> UploadAsync(int applicantId, UploadResumeDto dto);
    Task<IEnumerable<ResumeResponseDto>> ListByApplicantAsync(int applicantId);
    Task DeleteAsync(int resumeId);
}