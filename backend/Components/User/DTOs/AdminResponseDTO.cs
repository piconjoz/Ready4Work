namespace backend.User.DTOs;

public class AdminResponseDto
{
    public int AdminId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // user info included for convenience
    public UserResponseDto? User { get; set; }
}
