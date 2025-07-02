// backend/Components/Student/Models/StudentProfile.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.Student.Models
{
    public class StudentProfile
    {
        [Key]
        [Column("student_profile_id")]
        public int Id { get; internal set; }

        [Required]
        [MaxLength(20)]
        [Column("nric_fin")]
        public string NricFin { get; private set; } = string.Empty;

        [Required]
        [Column("student_id")]
        public int StudentId { get; private set; }

        [MaxLength(100)]
        [Column("nationality")]
        public string Nationality { get; private set; } = string.Empty;

        [Column("admit_year")]
        public int AdmitYear { get; private set; }

        [MaxLength(20)]
        [Column("primary_contact_number")]
        public string PrimaryContactNumber { get; private set; } = string.Empty;

        [MaxLength(10)]
        [Column("gender")]
        public string Gender { get; private set; } = string.Empty;

        [MaxLength(200)]
        [Column("degree_programme")]
        public string DegreeProgramme { get; private set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        [Column("full_name")]
        public string FullName { get; private set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("email")]
        public string Email { get; private set; } = string.Empty;

        // EF Core
        private StudentProfile() { }

        // Your own constructor
        public StudentProfile(
            string nricFin,
            int studentId,
            string nationality,
            int admitYear,
            string primaryContactNumber,
            string gender,
            string degreeProgramme,
            string fullName,
            string email)
        {
            NricFin            = nricFin;
            StudentId          = studentId;
            Nationality        = nationality;
            AdmitYear          = admitYear;
            PrimaryContactNumber = primaryContactNumber;
            Gender             = gender;
            DegreeProgramme    = degreeProgramme;
            FullName           = fullName;
            Email              = email;
        }

        // Example of a domain method if you ever need to update
        public void UpdateContact(string newNumber)
        {
            PrimaryContactNumber = newNumber;
        }
    }
}