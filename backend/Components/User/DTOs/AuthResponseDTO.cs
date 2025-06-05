namespace backend.User.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserResponseDto User { get; set; } = new();
    
    // optional: refresh token for enhanced security
    public string? RefreshToken { get; set; }
}
