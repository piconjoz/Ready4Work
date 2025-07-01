using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.JobListing.Models
{
    public class JobSkill
    {
        [Key]
        [Column("job_skill_id")]
        private int JobSkillId { get; set; }

        [Required]
        [Column("job_id")]
        private int JobId { get; set; }

        [Required]
        [Column("skill_id")]
        private int SkillId { get; set; }

        private JobSkill() { }

        internal JobSkill(int skill_Id, int job_id)
        {
            SkillId = skill_Id;
            JobId = job_id;
        }

        // Getter
        internal int GetJobSkillId() => JobSkillId;
        internal int GetJobId() => JobId;
        internal int GetSkillId() => SkillId;

        // Setter
        internal void SetJobSkillId(int jobSkillId) => JobSkillId = jobSkillId;
    }
}