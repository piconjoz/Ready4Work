using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.JobListing.Models
{
    public class Qualification
    {
        [Key]
        [Column("qualification_id")]
        private int QualificationId { get; set; }

        [Required]
        [Column("programme_id")]
        private int ProgrammeId { get; set; }

        [Required]
        [Column("job_id")]
        private int JobId { get; set; }

        private Qualification() { }

        internal Qualification(int qualification_id)
        {
            QualificationId = qualification_id;
        }

        // Getter
        internal int GetQualificationId() => QualificationId;
        internal int GetProgrammeId() => ProgrammeId;
        internal int GetJobId() => JobId;

        // Setter
        internal void SetQualificationId(int qualificationId) => QualificationId = qualificationId;
    }
}