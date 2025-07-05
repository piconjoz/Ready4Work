using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;   // add this

namespace backend.Components.ApplicantPreference.Models
{
    [Index(nameof(ApplicantId), IsUnique = true)]
    public class ApplicantPreference
    {
        [Key]
        public int PreferenceId { get; set; }

        // FK to Applicant table
        public int ApplicantId { get; set; }

        // Preference flags
        public bool RedactedResume { get; set; }
        public bool HideInfoUntilOffered { get; set; }
        public bool DisableDownload { get; set; }

        // Other settings
        public string RemunerationType { get; set; } = string.Empty;
        public string WorkingHoursStart { get; set; } = string.Empty;
        public string WorkingHoursEnd { get; set; } = string.Empty;

        // Audit
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}