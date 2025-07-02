namespace backend.User.DTOs;

public class AdminResponseDTO
{
    public int AdminId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // user info included for convenience
    public UserResponseDTO? User { get; set; }
}
