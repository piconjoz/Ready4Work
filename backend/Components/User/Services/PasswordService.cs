namespace backend.User.Services;

using System.Security.Cryptography;
using System.Text;
using backend.User.Services.Interfaces;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 32; // 32 bytes = 256 bits
    private const int HashIterations = 10000; // pbkdf2 iterations

    public (string salt, string hashedPassword) HashPassword(string plainPassword)
    {
        // generate a new salt for this password
        string salt = GenerateSalt();
        
        // hash the password with the salt
        string hashedPassword = HashPasswordWithSalt(plainPassword, salt);
        
        return (salt, hashedPassword);
    }

    public bool VerifyPassword(string plainPassword, string storedSalt, string storedHash)
    {
        // hash the provided password with the stored salt
        string hashedInput = HashPasswordWithSalt(plainPassword, storedSalt);
        
        // compare the hashes securely
        return SecureStringCompare(hashedInput, storedHash);
    }

    public string GenerateSalt()
    {
        // generate cryptographically secure random bytes
        byte[] saltBytes = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        
        // convert to base64 string for storage
        return Convert.ToBase64String(saltBytes);
    }

    private string HashPasswordWithSalt(string password, string salt)
    {
        // combine password and salt
        string combined = password + salt;
        byte[] combinedBytes = Encoding.UTF8.GetBytes(combined);
        
        // use pbkdf2 for key stretching (similar to bcrypt)
        using (var pbkdf2 = new Rfc2898DeriveBytes(combinedBytes, Encoding.UTF8.GetBytes(salt), HashIterations))
        {
            byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes = 256 bits
            return Convert.ToBase64String(hashBytes);
        }
    }

    // constant-time string comparison to prevent timing attacks
    private bool SecureStringCompare(string a, string b)
    {
        if (a.Length != b.Length)
            return false;

        int result = 0;
        for (int i = 0; i < a.Length; i++)
        {
            result |= a[i] ^ b[i]; // xor operation
        }
        
        return result == 0;
    }
}