using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.Qualification.Models
{
    public class Qualification
    {
        [Key]
        [Column("qualification_id")]
        public int QualificationId { get; set; }

        [Required]
        [Column("programme_id")]
        public int ProgrammeId { get; set; }

        [Required]
        [Column("job_id")]
        public int JobId { get; set; }

        public Qualification() { }

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