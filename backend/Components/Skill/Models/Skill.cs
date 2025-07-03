using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.Skill.Models
{
    public class Skill
    {
        [Key]
        [Column("skill_id")]
        public int SkillId { get; set; }

        [Column("skill")]
        public string skill { get; set; } = string.Empty;

        public Skill() { }

        internal Skill(string skill_name)
        {
            skill = skill_name;
        }

        // Getter
        internal int GetSkillId() => SkillId;
        internal string GetSkill() => skill;

        // Setter
        // internal void SetSkillId(int skillId) => SkillId = skillId;
        internal void SetSkill(string skill_name) => skill = skill_name;

    }
}