namespace backend.User.DTOs;

using System.ComponentModel.DataAnnotations;

public class ApplicantSignupDto
{
    [Required(ErrorMessage = "nric is required")]
    [StringLength(20, ErrorMessage = "nric cannot exceed 20 characters")]
    public string NRIC { get; set; } = string.Empty;

    [Required(ErrorMessage = "email is required")]
    [EmailAddress(ErrorMessage = "invalid email format")]
    [StringLength(255, ErrorMessage = "email cannot exceed 255 characters")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "first name is required")]
    [StringLength(100, ErrorMessage = "first name cannot exceed 100 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "last name is required")]
    [StringLength(100, ErrorMessage = "last name cannot exceed 100 characters")]
    public string LastName { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "phone cannot exceed 20 characters")]
    public string? Phone { get; set; }

    [StringLength(10, ErrorMessage = "gender cannot exceed 10 characters")]
    public string? Gender { get; set; }

    [Required(ErrorMessage = "password is required")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "password must be between 8 and 255 characters")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "programme id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "programme id must be a positive number")]
    public int ProgrammeId { get; set; }

    [Required(ErrorMessage = "admit year is required")]
    [Range(2020, 2030, ErrorMessage = "admit year must be between 2020 and 2030")]
    public int AdmitYear { get; set; }
}