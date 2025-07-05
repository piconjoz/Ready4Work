namespace backend.Components.Student.DTOs
{
    public class StudentProfileDTO
    {
        public string NricFin { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public int AdmitYear { get; set; }
        public string PrimaryContactNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string DegreeProgramme { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}