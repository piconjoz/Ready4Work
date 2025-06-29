using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.JobListing.Models
{
    public class Programme
    {
        [Key]
        [Column("programme_id")]
        private int ProgrammeId { get; set; }

        [Column("programme_name")]
        private string ProgrammeName { get; set; } = string.Empty;

        private Programme() { }

        internal Programme(int programme_id, string programme_name)
        {
            ProgrammeId = programme_id;
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