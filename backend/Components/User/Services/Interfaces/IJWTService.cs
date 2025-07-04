namespace backend.User.Services.Interfaces;

using System.Security.Claims;

public interface IJWTService
{
    // generates a jwt token for a user
    string GenerateToken(int userId, int userType, int? applicantId = null);
    
    // validates a jwt token and returns claims if valid
    ClaimsPrincipal? ValidateToken(string token);
    
    // extracts user id from jwt token
    int? GetUserIdFromToken(string token);
    
    // checks if token is expired
    bool IsTokenExpired(string token);
}