using System.Threading.Tasks;
using backend.Components.ApplicantPreference.Models;
using ApplicantPref = backend.Components.ApplicantPreference.Models.ApplicantPreference;

namespace backend.Components.ApplicantPreference.Repositories.Interfaces
{
    public interface IApplicantPreferenceRepository
    {
        Task<ApplicantPref?> GetByApplicantIdAsync(int applicantId);
        Task<ApplicantPref>  CreateOrUpdateAsync(ApplicantPref pref);
    }
}