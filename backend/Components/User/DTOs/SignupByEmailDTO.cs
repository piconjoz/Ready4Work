using System.ComponentModel.DataAnnotations;

namespace backend.User.DTOs
{
    public class SignupByEmailDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
    }
}