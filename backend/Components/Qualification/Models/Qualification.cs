using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.Qualification.Models
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

        internal Qualification(int programmeId, int jobId)
        {
            ProgrammeId = programmeId;
            JobId = jobId;
        }

        // Getter
        internal int GetQualificationId() => QualificationId;
        internal int GetProgrammeId() => ProgrammeId;
        internal int GetJobId() => JobId;

        // Setter
        internal void SetQualificationId(int qualificationId) => QualificationId = qualificationId;
    }
}