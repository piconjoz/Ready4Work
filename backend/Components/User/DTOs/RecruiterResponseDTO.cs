namespace backend.User.DTOs;

public class RecruiterResponseDTO
{
    public int RecruiterId { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string? Department { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // user info included for convenience
    public UserResponseDTO? User { get; set; }
}
