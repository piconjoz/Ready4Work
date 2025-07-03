using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.Programme.Models
{
    public class Programme
    {
        [Key]
        [Column("programme_id")]
        public int ProgrammeId { get; set; }

        [Column("programme_name")]
        public string ProgrammeName { get; set; } = string.Empty;

        public Programme() { }

        internal Programme(string programme_name)
        {
            ProgrammeName = programme_name;
        }

        // Getter
        internal int GetProgrammeId() => ProgrammeId;
        internal string GetProgrammeName() => ProgrammeName;

        // Setter
        internal void SetProgrammeId(int programmeId) => ProgrammeId = programmeId;
        internal void SetProgrammeName(string programmeName) => ProgrammeName = programmeName;
    }
}