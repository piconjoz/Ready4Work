using System.ComponentModel.DataAnnotations;

namespace backend.User.DTOs
{
    public class RecruiterOnboardingDTO
    {
        [Required]
        public CompanyDTO Company { get; set; } = new CompanyDTO();

        [Required]
        public UserDTO User { get; set; } = new UserDTO();

        [Required]
        public RecruiterDTO Recruiter { get; set; } = new RecruiterDTO();

        public MetadataDTO? Metadata { get; set; }
    }

    public class CompanyDTO
    {
        public string CompanyName { get; set; } = string.Empty;
        public string PreferredCompanyName { get; set; } = string.Empty;
        public string CompanyDescription { get; set; } = string.Empty;
        public string CountryOfBusinessRegistration { get; set; } = string.Empty;
        public string UEN { get; set; } = string.Empty;
        public int NumberOfEmployees { get; set; }
        public string IndustryCluster { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public bool AuthorisedTrainingOrganisation { get; set; }
        public string CompanyWebsite { get; set; } = string.Empty;
        public string CompanyContact { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZoneLocation { get; set; } = string.Empty;
        public int CountryCode { get; set; }
        public string UnitNumber { get; set; } = string.Empty;
        public string Floor { get; set; } = string.Empty;
        public int AreaCode { get; set; }
        public string Block { get; set; } = string.Empty;
        public int PostalCode { get; set; }
        public string EmploymentType { get; set; } = string.Empty;
    }

    public class UserDTO
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int UserType { get; set; }
        public string Password { get; set; } = string.Empty;
    }

    public class RecruiterDTO
    {
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    public class MetadataDTO
    {
        public string RegistrationDate { get; set; } = string.Empty;
        public string SubmissionTimestamp { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
    }
}
