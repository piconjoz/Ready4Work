using System.Threading.Tasks;
using backend.Components.ApplicantPreference.Models;
using backend.Components.ApplicantPreference.DTOs;
using backend.Components.ApplicantPreference.Repositories.Interfaces;
using backend.Components.ApplicantPreference.Services.Interfaces;
using ApplicantPref = backend.Components.ApplicantPreference.Models.ApplicantPreference;

namespace backend.Components.ApplicantPreference.Services
{
    public class ApplicantPreferenceService : IApplicantPreferenceService
    {
        private readonly IApplicantPreferenceRepository _repo;
        public ApplicantPreferenceService(IApplicantPreferenceRepository repo) => _repo = repo;

        public async Task<ApplicantPreferenceDTO?> GetByApplicantIdAsync(int applicantId)
        {
            var pref = await _repo.GetByApplicantIdAsync(applicantId);
            return pref == null ? null : Map(pref);
        }

        public async Task<ApplicantPreferenceDTO> CreateOrUpdateAsync(ApplicantPreferenceDTO dto)
        {
            var entity = new ApplicantPref
            {
                ApplicantId          = dto.ApplicantId,
                RedactedResume       = dto.RedactedResume,
                HideInfoUntilOffered = dto.HideInfoUntilOffered,
                DisableDownload      = dto.DisableDownload,
                RemunerationType     = dto.RemunerationType,
                WorkingHoursStart    = dto.WorkingHoursStart,
                WorkingHoursEnd      = dto.WorkingHoursEnd
            };

            var saved = await _repo.CreateOrUpdateAsync(entity);
            return Map(saved);
        }

        private static ApplicantPreferenceDTO Map(ApplicantPref p) => new()
        {
            PreferenceId         = p.PreferenceId,
            ApplicantId          = p.ApplicantId,
            RedactedResume       = p.RedactedResume,
            HideInfoUntilOffered = p.HideInfoUntilOffered,
            DisableDownload      = p.DisableDownload,
            RemunerationType     = p.RemunerationType,
            WorkingHoursStart    = p.WorkingHoursStart,
            WorkingHoursEnd      = p.WorkingHoursEnd,
            CreatedAt            = p.CreatedAt,
            UpdatedAt            = p.UpdatedAt
        };
    }
}