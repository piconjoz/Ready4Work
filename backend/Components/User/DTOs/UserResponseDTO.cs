namespace backend.User.DTOs;

public class UserResponseDTO
{
    public int UserId { get; set; }
    public string NRIC { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Gender { get; set; }
    public string? ProfilePicturePath { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsVerified { get; set; }
    public int UserType { get; set; } // 1=applicant, 2=recruiter, 3=admin
}
