namespace backend.User.Services;

using backend.User.Models;
using backend.User.DTOs;
using backend.User.Services.Interfaces;
using backend.User.Repositories.Interfaces;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public UserService(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<User> CreateUserAsync(string nric, string email, string firstName, string lastName, 
                                           string? phone, string? gender, int userType, string password)
    {
        // validate input
        if (string.IsNullOrWhiteSpace(nric)) throw new ArgumentException("nric cannot be empty");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("email cannot be empty");
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("first name cannot be empty");
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("last name cannot be empty");
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("password cannot be empty");

        // check if email already exists
        if (await EmailExistsAsync(email))
        {
            throw new InvalidOperationException("email already exists");
        }

        // check if nric already exists
        if (await NricExistsAsync(nric))
        {
            throw new InvalidOperationException("nric already exists");
        }

        // hash the password
        var (salt, hashedPassword) = _passwordService.HashPassword(password);

        // create the user entity
        var user = new User(nric, email, firstName, lastName, phone, gender, userType, salt, hashedPassword);

        // save to database
        return await _userRepository.CreateAsync(user);
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        if (userId <= 0) return null;
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return null;
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<User?> GetUserByNricAsync(string nric)
    {
        if (string.IsNullOrWhiteSpace(nric)) return null;
        return await _userRepository.GetByNricAsync(nric);
    }

    public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) 
            return false;

        var user = await GetUserByEmailAsync(email);
        if (user == null || !user.GetIsActive()) 
            return false;

        return _passwordService.VerifyPassword(password, user.GetSalt(), user.GetPasswordHash());
    }

    public async Task<User?> AuthenticateUserAsync(string email, string password)
    {
        if (!await ValidateUserCredentialsAsync(email, password))
            return null;

        return await GetUserByEmailAsync(email);
    }

    public async Task<User> UpdateUserDetailsAsync(int userId, string firstName, string lastName, string? phone, string? gender)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null) throw new ArgumentException("user not found");

        // validate input
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("first name cannot be empty");
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("last name cannot be empty");

        // update user details using setter methods
        user.SetFirstName(firstName);
        user.SetLastName(lastName);
        user.SetPhone(phone);
        user.SetGender(gender);
        user.SetUpdatedAt(DateTime.UtcNow);

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<User> UpdateUserPasswordAsync(int userId, string newPassword)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null) throw new ArgumentException("user not found");

        if (string.IsNullOrWhiteSpace(newPassword)) throw new ArgumentException("password cannot be empty");

        // hash the new password
        var (salt, hashedPassword) = _passwordService.HashPassword(newPassword);

        // update password using setter methods
        user.SetSalt(salt);
        user.SetPasswordHash(hashedPassword);
        user.SetUpdatedAt(DateTime.UtcNow);

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<User> VerifyUserAsync(int userId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null) throw new ArgumentException("user not found");

        user.SetIsVerified(true);
        user.SetUpdatedAt(DateTime.UtcNow);

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<User> DeactivateUserAsync(int userId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null) throw new ArgumentException("user not found");

        user.SetIsActive(false);
        user.SetUpdatedAt(DateTime.UtcNow);

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<User> UpdateProfilePictureAsync(int userId, string profilePicturePath)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null) throw new ArgumentException("user not found");

        user.SetProfilePicturePath(profilePicturePath);
        user.SetUpdatedAt(DateTime.UtcNow);

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return await _userRepository.ExistsByEmailAsync(email);
    }

    public async Task<bool> NricExistsAsync(string nric)
    {
        if (string.IsNullOrWhiteSpace(nric)) return false;
        return await _userRepository.ExistsByNricAsync(nric);
    }

    public UserResponseDto ConvertToResponseDto(User user)
    {
        return new UserResponseDto
        {
            UserId = user.GetUserId(),
            NRIC = user.GetNRIC(),
            Email = user.GetEmail(),
            FirstName = user.GetFirstName(),
            LastName = user.GetLastName(),
            Phone = user.GetPhone(),
            Gender = user.GetGender(),
            ProfilePicturePath = user.GetProfilePicturePath(),
            IsActive = user.GetIsActive(),
            CreatedAt = user.GetCreatedAt(),
            UpdatedAt = user.GetUpdatedAt(),
            IsVerified = user.GetIsVerified(),
            UserType = user.GetUserType()
        };
    }
}