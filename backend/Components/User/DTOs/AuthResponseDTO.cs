namespace backend.User.DTOs;

public class AuthResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserResponseDTO User { get; set; } = new();
    
    // optional: refresh token for enhanced security
    public string? RefreshToken { get; set; }
}