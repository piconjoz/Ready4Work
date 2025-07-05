using System;

namespace backend.Components.ApplicantPreference.DTOs
{
    /// <summary>
    /// Single DTO used for both POST (create/update) and GET.
    /// </summary>
    public class ApplicantPreferenceDTO
    {
        // Primary-key (null when creating)
        public int? PreferenceId { get; set; }

        // Required for create/update
        public int  ApplicantId  { get; set; }

        public bool RedactedResume       { get; set; }
        public bool HideInfoUntilOffered { get; set; }
        public bool DisableDownload      { get; set; }
        public string RemunerationType   { get; set; } = string.Empty;
        public string WorkingHoursStart  { get; set; } = string.Empty;
        public string WorkingHoursEnd    { get; set; } = string.Empty;

        // Filled on read
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}