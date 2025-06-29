using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.JobListing.Models
{
    public class Skill
    {
        [Key]
        [Column("skill_id")]
        private int SkillId { get; set; }

        [Column("skill")]
        private string skill { get; set; } = string.Empty;

        private Skill() { }

        internal Skill(int skillId, string skill_name)
        {
            SkillId = skillId;
            skill = skill_name;
        }

        // Getter
        internal int GetSkillId() => SkillId;
        internal string GetSkill() => skill;

        // Setter
        internal void SetSkillId(int skillId) => SkillId = skillId;
        internal void SetSkill(string skill_name) => skill = skill_name;

    }
}