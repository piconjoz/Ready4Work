namespace backend.User.Services;

using backend.User.DTOs;
using backend.User.Models;
using backend.User.Services.Interfaces;
using backend.User.Repositories.Interfaces;

public class AdminAccountManagementService : IAdminAccountManagementService
{
    private readonly IUserService _userService;
    private readonly IApplicantService _applicantService;
    private readonly IRecruiterService _recruiterService;
    private readonly IAdminService _adminService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AdminAccountManagementService> _logger;

    public AdminAccountManagementService(
        IUserService userService,
        IApplicantService applicantService,
        IRecruiterService recruiterService,
        IAdminService adminService,
        IUserRepository userRepository,
        ILogger<AdminAccountManagementService> logger)
    {
        _userService = userService;
        _applicantService = applicantService;
        _recruiterService = recruiterService;
        _adminService = adminService;
        _userRepository = userRepository;
        _logger = logger;
    }

    // CREATE - Create new user account
    public async Task<AccountManagementResponseDTO> CreateUserAccountAsync(CreateUserAccountDTO dto, int adminId)
    {
        try
        {
            _logger.LogInformation($"Admin {adminId} creating user account for {dto.Email}");

            // 1. Check if email already exists
            if (await EmailExistsAsync(dto.Email))
            {
                throw new InvalidOperationException($"A user with email '{dto.Email}' already exists.");
            }

            // 2. Validate role-specific data
            ValidateRoleSpecificData(dto);

            // 3. Create the user account
            var user = await _userService.CreateUserAsync(
                email: dto.Email,
                firstName: dto.FirstName,
                lastName: dto.LastName,
                phone: dto.Phone,
                gender: dto.Gender,
                userType: dto.UserType,
                password: dto.Password
            );

            _logger.LogInformation($"User {user.GetUserId()} created successfully by admin {adminId}");

            // 4. Create role-specific profile
            await CreateRoleSpecificProfileAsync(user.GetUserId(), dto);

            // 5. Return response
            return await ConvertToResponseDTOAsync(user);
        }
        catch (InvalidOperationException)
        {
            throw; // Re-throw business logic exceptions
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning($"Validation error creating user: {ex.Message}");
            throw new InvalidOperationException($"Validation failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error creating user account for {dto.Email}");
            throw new InvalidOperationException("An unexpected error occurred while creating the user account.");
        }
    }

    // READ - Get all users
    public async Task<List<AccountManagementResponseDTO>> GetAllUsersAsync()
    {
        try
        {
            var users = await _userRepository.GetActiveUsersAsync();
            var responseDtos = new List<AccountManagementResponseDTO>();
            
            foreach (var user in users)
            {
                var dto = await ConvertToResponseDTOAsync(user);
                responseDtos.Add(dto);
            }

            return responseDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all users");
            throw new InvalidOperationException("An error occurred while retrieving users.");
        }
    }

    // READ - Get specific user
    public async Task<AccountManagementResponseDTO?> GetUserAccountAsync(int userId)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return await ConvertToResponseDTOAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving user {userId}");
            throw new InvalidOperationException($"An error occurred while retrieving user {userId}.");
        }
    }

    // READ - Get users by type
    public async Task<List<AccountManagementResponseDTO>> GetUsersByTypeAsync(int userType)
    {
        try
        {
            var users = await _userRepository.GetUsersByTypeAsync(userType);
            var responses = new List<AccountManagementResponseDTO>();
            
            foreach (var user in users)
            {
                responses.Add(await ConvertToResponseDTOAsync(user));
            }
            
            return responses;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving users by type {userType}");
            throw new InvalidOperationException($"An error occurred while retrieving users by type.");
        }
    }

    // UPDATE - Update user account
    public async Task<AccountManagementResponseDTO> UpdateUserAccountAsync(int userId, UpdateUserAccountDTO dto, int adminId)
    {
        try
        {
            _logger.LogInformation($"Admin {adminId} updating user {userId}");

            // 1. Get existing user
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} not found.");
            }

            // 2. Update basic user details
            var updatedUser = await _userService.UpdateUserDetailsAsync(
                userId: userId,
                firstName: dto.FirstName,
                lastName: dto.LastName,
                phone: dto.Phone,
                gender: dto.Gender
            );

            // 3. Update status if changed
            if (dto.IsActive != user.GetIsActive())
            {
                if (!dto.IsActive)
                {
                    await _userService.DeactivateUserAsync(userId);
                    _logger.LogInformation($"User {userId} deactivated by admin {adminId}");
                }
                // Note: Activation would need ActivateUserAsync method in UserService
            }

            // 4. Update verification status if changed
            if (dto.IsVerified != user.GetIsVerified() && dto.IsVerified)
            {
                await _userService.VerifyUserAsync(userId);
                _logger.LogInformation($"User {userId} verified by admin {adminId}");
            }

            // 5. Update role-specific data if provided
            await UpdateRoleSpecificDataAsync(userId, user.GetUserType(), dto);

            return await ConvertToResponseDTOAsync(updatedUser);
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating user {userId}");
            throw new InvalidOperationException($"An error occurred while updating user {userId}.");
        }
    }

    // DELETE - Delete user account (soft delete via deactivation)
    public async Task<bool> DeleteUserAccountAsync(int userId, int adminId)
    {
        try
        {
            _logger.LogInformation($"Admin {adminId} attempting to delete user {userId}");

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} not found.");
            }

            // Prevent admin from deleting themselves
            if (user.GetUserType() == 3)
            {
                var admin = await _adminService.GetAdminByUserIdAsync(userId);
                if (admin != null && admin.GetUserId() == adminId)
                {
                    throw new InvalidOperationException("You cannot delete your own admin account.");
                }
            }

            // Soft delete by deactivating
            await _userService.DeactivateUserAsync(userId);
            
            _logger.LogInformation($"User {userId} deleted (deactivated) by admin {adminId}");
            return true;
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting user {userId}");
            throw new InvalidOperationException($"An error occurred while deleting user {userId}.");
        }
    }

    // Helper method - Check if email exists
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _userService.EmailExistsAsync(email);
    }

    // ============================================================================
    // Private Helper Methods
    // ============================================================================

    private void ValidateRoleSpecificData(CreateUserAccountDTO dto)
    {
        switch (dto.UserType)
        {
            case 1: // Applicant
                if (dto.ApplicantData == null)
                {
                    throw new InvalidOperationException("Applicant data is required for applicant accounts.");
                }
                break;

            case 2: // Recruiter
                if (dto.RecruiterData == null)
                {
                    throw new InvalidOperationException("Recruiter data is required for recruiter accounts.");
                }
                break;

            case 3: // Admin
                // No additional data required for admin
                break;

            default:
                throw new InvalidOperationException($"Invalid user type: {dto.UserType}");
        }
    }

    private async Task CreateRoleSpecificProfileAsync(int userId, CreateUserAccountDTO dto)
    {
        switch (dto.UserType)
        {
            case 1: // Applicant
                if (dto.ApplicantData != null)
                {
                    await _applicantService.CreateApplicantAsync(
                        userId,
                        dto.ApplicantData.ProgrammeId,
                        dto.ApplicantData.AdmitYear
                    );
                }
                break;

            case 2: // Recruiter
                if (dto.RecruiterData != null)
                {
                    await _recruiterService.CreateRecruiterAsync(
                        userId,
                        dto.RecruiterData.CompanyId,
                        dto.RecruiterData.JobTitle,
                        dto.RecruiterData.Department
                    );
                }
                break;

            case 3: // Admin
                await _adminService.CreateAdminAsync(userId);
                break;
        }
    }

    private async Task<AccountManagementResponseDTO> ConvertToResponseDTOAsync(User user)
    {
        var response = new AccountManagementResponseDTO
        {
            UserId = user.GetUserId(),
            Email = user.GetEmail(),
            FullName = $"{user.GetFirstName()} {user.GetLastName()}",
            UserType = user.GetUserType(),
            UserTypeDisplay = GetUserTypeDisplay(user.GetUserType()),
            IsActive = user.GetIsActive(),
            IsVerified = user.GetIsVerified(),
            CreatedAt = user.GetCreatedAt(),
            UpdatedAt = user.GetUpdatedAt()
        };

        // Add role-specific data
        response.RoleData = await GetRoleSpecificDataAsync(user.GetUserId(), user.GetUserType());

        return response;
    }

    private async Task<object?> GetRoleSpecificDataAsync(int userId, int userType)
    {
        try
        {
            switch (userType)
            {
                case 1: // Applicant
                    var applicant = await _applicantService.GetApplicantByUserIdAsync(userId);
                    return applicant != null ? new
                    {
                        ApplicantId = applicant.GetApplicantId(),
                        ProgrammeId = applicant.GetProgrammeId(),
                        AdmitYear = applicant.GetAdmitYear()
                    } : null;

                case 2: // Recruiter
                    var recruiter = await _recruiterService.GetRecruiterByUserIdAsync(userId);
                    return recruiter != null ? new
                    {
                        RecruiterId = recruiter.GetRecruiterId(),
                        CompanyId = recruiter.GetCompanyId(),
                        JobTitle = recruiter.GetJobTitle(),
                        Department = recruiter.GetDepartment()
                    } : null;

                case 3: // Admin
                    var admin = await _adminService.GetAdminByUserIdAsync(userId);
                    return admin != null ? new
                    {
                        AdminId = admin.GetAdminId()
                    } : null;

                default:
                    return null;
            }
        }
        catch
        {
            return null; // If role data can't be retrieved, return null
        }
    }

    private string GetUserTypeDisplay(int userType)
    {
        return userType switch
        {
            1 => "Applicant",
            2 => "Recruiter",
            3 => "Admin",
            _ => "Unknown"
        };
    }

    private async Task UpdateRoleSpecificDataAsync(int userId, int userType, UpdateUserAccountDTO dto)
    {
        switch (userType)
        {
            case 1: // Applicant
                if (dto.ApplicantData != null)
                {
                    var applicant = await _applicantService.GetApplicantByUserIdAsync(userId);
                    if (applicant != null)
                    {
                        await _applicantService.UpdateApplicantDetailsAsync(
                            applicant.GetApplicantId(),
                            dto.ApplicantData.ProgrammeId,
                            dto.ApplicantData.AdmitYear
                        );
                    }
                }
                break;

            case 2: // Recruiter
                if (dto.RecruiterData != null)
                {
                    var recruiter = await _recruiterService.GetRecruiterByUserIdAsync(userId);
                    if (recruiter != null)
                    {
                        await _recruiterService.UpdateRecruiterDetailsAsync(
                            recruiter.GetRecruiterId(),
                            dto.RecruiterData.CompanyId,
                            dto.RecruiterData.JobTitle,
                            dto.RecruiterData.Department
                        );
                    }
                }
                break;

            case 3: // Admin
                // No role-specific data to update for admin
                break;
        }
    }
}