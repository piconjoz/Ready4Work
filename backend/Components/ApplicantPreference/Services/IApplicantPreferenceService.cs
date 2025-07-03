using System.Threading.Tasks;
using backend.Components.ApplicantPreference.DTOs;

namespace backend.Components.ApplicantPreference.Services.Interfaces
{
    public interface IApplicantPreferenceService
    {
        Task<ApplicantPreferenceDTO?> GetByApplicantIdAsync(int applicantId);
        Task<ApplicantPreferenceDTO>  CreateOrUpdateAsync(ApplicantPreferenceDTO dto);
    }
}