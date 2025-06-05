namespace backend.User.DTOs;

using System.ComponentModel.DataAnnotations;

public class RecruiterSignupDto
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

    [Required(ErrorMessage = "company id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "company id must be a positive number")]
    public int CompanyId { get; set; }

    [Required(ErrorMessage = "job title is required")]
    [StringLength(100, ErrorMessage = "job title cannot exceed 100 characters")]
    public string JobTitle { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "department cannot exceed 100 characters")]
    public string? Department { get; set; }
}