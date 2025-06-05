namespace backend.User.DTOs;

using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required(ErrorMessage = "email is required")]
    [EmailAddress(ErrorMessage = "invalid email format")]
    [StringLength(255, ErrorMessage = "email cannot exceed 255 characters")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "password is required")]
    [StringLength(255, ErrorMessage = "password cannot exceed 255 characters")]
    public string Password { get; set; } = string.Empty;
}