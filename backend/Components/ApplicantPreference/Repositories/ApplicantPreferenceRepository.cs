using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Components.ApplicantPreference.Models;
using ApplicantPref = backend.Components.ApplicantPreference.Models.ApplicantPreference;
using backend.Components.ApplicantPreference.Repositories.Interfaces;

namespace backend.Components.ApplicantPreference.Repositories
{
    public class ApplicantPreferenceRepository : IApplicantPreferenceRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicantPreferenceRepository(ApplicationDbContext db) => _db = db;

        public async Task<ApplicantPref?> GetByApplicantIdAsync(int applicantId) =>
            await _db.ApplicantPreferences
                     .FirstOrDefaultAsync(p => p.ApplicantId == applicantId);

        public async Task<ApplicantPref> CreateOrUpdateAsync(ApplicantPref pref)
        {
            var existing = await _db.ApplicantPreferences
                                    .SingleOrDefaultAsync(p => p.ApplicantId == pref.ApplicantId);

            if (existing == null)
            {
                pref.CreatedAt = DateTime.UtcNow;
                pref.UpdatedAt = pref.CreatedAt;
                _db.ApplicantPreferences.Add(pref);
                await _db.SaveChangesAsync();
                return pref;
            }

            // update tracked entity
            existing.RedactedResume       = pref.RedactedResume;
            existing.HideInfoUntilOffered = pref.HideInfoUntilOffered;
            existing.DisableDownload      = pref.DisableDownload;
            existing.RemunerationType     = pref.RemunerationType;
            existing.WorkingHoursStart    = pref.WorkingHoursStart;
            existing.WorkingHoursEnd      = pref.WorkingHoursEnd;
            existing.UpdatedAt            = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return existing;
        }
    }
}