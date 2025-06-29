namespace backend.User.Services;

using backend.User.Models;
using backend.User.DTOs;
using backend.User.Services.Interfaces;
using backend.User.Repositories.Interfaces;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IUserService _userService;
    
    public AdminService(IAdminRepository adminRepository, IUserService userService)
    {
        _adminRepository = adminRepository;
        _userService = userService;
    }
    
    // Creates an admin profile - called during admin creation
    // Only creates the admin record, but user record must first exist
    public async Task<Admin> CreateAdminAsync(int userId)
    {
        // Validates that the user exists
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) throw new ArgumentException("User does not exist");
        
        // Validate user is actually an admin type
        if (user.GetUserType() != 3)
        {
            throw new ArgumentException("User must have admin user type (3)");
        }
        
        // Validates that the user is not already an admin
        if (await ExistsByUserIdAsync(userId))
        {
            throw new InvalidOperationException("User already has an admin profile");
        }

        var admin = new Admin(userId);
        
        // Save the admin record to db
        return await _adminRepository.CreateAsync(admin);
    }
    
    // Retrieves an admin by their ID
    public async Task<Admin?> GetAdminByIdAsync(int adminId)
    {
        if (adminId <= 0) return null;
        return await _adminRepository.GetByIdAsync(adminId);
    }
    
    // Retrieves an admin by their user ID
    public async Task<Admin?> GetAdminByUserIdAsync(int userId)
    {
        if (userId <= 0) return null;
        return await _adminRepository.GetByUserIdAsync(userId);
    }
    
    // Updates an admin's details (minimal for admin)
    public async Task<Admin> UpdateAdminAsync(int adminId)
    {
        var admin = await GetAdminByIdAsync(adminId);
        if (admin == null) throw new ArgumentException("Admin does not exist");
        
        admin.SetUpdatedAt(DateTime.UtcNow);
        
        return await _adminRepository.UpdateAsync(admin);
    }
    
    // Deletes an admin profile
    public async Task<bool> DeleteAdminAsync(int adminId)
    {
        if (adminId <= 0) return false;
        return await _adminRepository.DeleteAsync(adminId);
    }

    // Checks if user has admin profile
    public async Task<bool> ExistsByUserIdAsync(int userId)
    {
        if (userId <= 0) return false;
        return await _adminRepository.ExistsByUserIdAsync(userId);
    }
    
    // Gets all admins - used for admin management
    public async Task<List<Admin>> GetAllAdminsAsync()
    {
        return await _adminRepository.GetAllAdminsAsync();
    }
    
    // Converts to DTO with user data
    // Combines admin data with user profile for frontend
    public async Task<AdminResponseDTO> ConvertToResponseDTOAsync(Admin admin)
    {
        var user = await _userService.GetUserByIdAsync(admin.GetUserId());
        
        return new AdminResponseDTO
        {
            AdminId = admin.GetAdminId(),
            UserId = admin.GetUserId(),
            CreatedAt = admin.GetCreatedAt(),
            UpdatedAt = admin.GetUpdatedAt(),
            User = user != null ? _userService.ConvertToResponseDTO(user) : null
        };
    }
    
    // Gets complete admin profile with user data - used by controllers
    public async Task<AdminResponseDTO?> GetAdminProfileAsync(int userId)
    {
        var admin = await GetAdminByUserIdAsync(userId);
        if (admin == null) return null;

        return await ConvertToResponseDTOAsync(admin);
    }

    // Creates complete admin account (User + Admin) - used for seeding
    public async Task<AdminResponseDTO> CreateCompleteAdminAccountAsync(
        string email, 
        string firstName, 
        string lastName, 
        string? phone, 
        string? gender, 
        string password)
    {
        // Step 1: Create user with admin type
        var user = await _userService.CreateUserAsync(
            email, 
            firstName, 
            lastName, 
            phone, 
            gender, 
            3, // UserType = 3 for admin
            password
        );

        // Step 2: Create admin profile
        var admin = await CreateAdminAsync(user.GetUserId());

        // Step 3: Return complete profile
        return await ConvertToResponseDTOAsync(admin);
    }
}