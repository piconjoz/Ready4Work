namespace backend.User.Services.Interfaces;

public interface IPasswordService
{
    // generates a new salt and hashes the password
    (string salt, string hashedPassword) HashPassword(string plainPassword);
    
    // verifies a password against stored salt and hash
    bool VerifyPassword(string plainPassword, string storedSalt, string storedHash);
    
    // generates a cryptographically secure salt
    string GenerateSalt();
}