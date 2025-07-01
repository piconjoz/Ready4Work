namespace backend.User.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.User.DTOs;
using backend.User.Services.Interfaces;
using backend.Components.Company.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/admins")]
[Authorize] // All endpoints require authentication
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IUserService _userService;
    private readonly IApplicantService _applicantService;
    private readonly IRecruiterService _recruiterService;
    private readonly ICompanyService _companyService;
    private readonly IAdminAccountManagementService _accountManagementService;


    public AdminController(
        IAdminService adminService,
        IUserService userService,
        IApplicantService applicantService,
        IRecruiterService recruiterService,
        ICompanyService companyService,
        IAdminAccountManagementService accountManagementService)
    {
        _adminService = adminService;
        _userService = userService;
        _applicantService = applicantService;
        _recruiterService = recruiterService;
        _companyService = companyService;
        _accountManagementService = accountManagementService;
        
    }

    // GET /api/admins/profile
    // Gets current admin's complete profile (user + admin data)
    [HttpGet("profile")]
    public async Task<ActionResult<AdminResponseDTO>> GetProfile()
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            // Check if user is actually an admin
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can access this endpoint");
            }

            var adminProfile = await _adminService.GetAdminProfileAsync(userId.Value);
            if (adminProfile == null)
            {
                return NotFound(new { message = "Admin profile not found" });
            }

            return Ok(adminProfile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving admin profile" });
        }
    }

    // GET /api/admins/dashboard
    // Gets dashboard statistics for admin panel
    [HttpGet("dashboard")]
    public async Task<ActionResult<AdminDashboardDTO>> GetDashboard()
    {
        try
        {
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can access this endpoint");
            }

            // Get all the statistics
            var allAdmins = await _adminService.GetAllAdminsAsync();
            var companies = await _companyService.GetAllCompaniesAsync();

            // Note: You'll need to add these methods to your services
            // var allApplicants = await _applicantService.GetAllApplicantsAsync();
            // var allRecruiters = await _recruiterService.GetAllRecruitersAsync();

            var dashboard = new AdminDashboardDTO
            {
                TotalAdmins = allAdmins.Count,
                TotalCompanies = companies.Count,
                // TotalApplicants = allApplicants.Count,
                // TotalRecruiters = allRecruiters.Count,
                TotalApplicants = 0, // Placeholder until you add the method
                TotalRecruiters = 0, // Placeholder until you add the method
                LastUpdated = DateTime.UtcNow
            };

            return Ok(dashboard);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving dashboard data" });
        }
    }

    // GET /api/admins/users
    // Gets all users (for admin user management)
    [HttpGet("users")]
    public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers([FromQuery] int userType = 0)
    {
        try
        {
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can access this endpoint");
            }

            // Note: You'll need to add this method to IUserService
            // For now, this is a placeholder
            return BadRequest("This endpoint requires additional implementation in UserService");

            // Future implementation:
            // var users = userType > 0 
            //     ? await _userService.GetUsersByTypeAsync(userType)
            //     : await _userService.GetAllUsersAsync();
            
            // var userDtos = users.Select(user => _userService.ConvertToResponseDTO(user)).ToList();
            // return Ok(userDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving users" });
        }
    }

    // PUT /api/admins/users/{userId}/activate
    // Activates or deactivates a user account
    [HttpPut("users/{userId}/activate")]
    public async Task<ActionResult> ToggleUserActivation(int userId, [FromBody] ToggleActivationRequest request)
    {
        try
        {
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can access this endpoint");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userId <= 0)
            {
                return BadRequest(new { message = "Invalid user ID" });
            }

            // Prevent admins from deactivating themselves
            var currentUserId = GetUserIdFromToken();
            if (currentUserId == userId && !request.IsActive)
            {
                return BadRequest(new { message = "You cannot deactivate your own account" });
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            if (request.IsActive)
            {
                // Note: You'll need to add an ActivateUserAsync method to IUserService
                // await _userService.ActivateUserAsync(userId);
                return Ok(new { message = "User activation method not implemented yet" });
            }
            else
            {
                await _userService.DeactivateUserAsync(userId);
                return Ok(new { message = "User deactivated successfully" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating user status" });
        }
    }

    // GET /api/admins/companies
    // Gets all companies for admin management
    [HttpGet("companies")]
    public async Task<ActionResult<List<backend.Components.Company.Models.Company>>> GetAllCompanies()
    {
        try
        {
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can access this endpoint");
            }

            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving companies" });
        }
    }

    // DELETE /api/admins/companies/{companyId}
    // Deletes a company (admin only)
    [HttpDelete("companies/{companyId}")]
    public async Task<ActionResult> DeleteCompany(int companyId)
    {
        try
        {
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can access this endpoint");
            }

            if (companyId <= 0)
            {
                return BadRequest(new { message = "Invalid company ID" });
            }

            var result = await _companyService.DeleteCompanyAsync(companyId);
            if (!result)
            {
                return NotFound(new { message = "Company not found" });
            }

            return Ok(new { message = "Company deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting company" });
        }
    }

     // CREATE - POST /api/admins/accounts
    [HttpPost("accounts")]
    public async Task<ActionResult<AccountManagementResponseDTO>> CreateUserAccount([FromBody] CreateUserAccountDTO request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adminUserId = GetUserIdFromToken();
            if (adminUserId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can create user accounts");
            }

            var result = await _accountManagementService.CreateUserAccountAsync(request, adminUserId.Value);
            
            return CreatedAtAction(
                nameof(GetUserAccount), 
                new { userId = result.UserId }, 
                result
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the user account" });
        }
    }

    // READ - GET /api/admins/accounts
    [HttpGet("accounts")]
    public async Task<ActionResult<List<AccountManagementResponseDTO>>> GetAllUserAccounts()
    {
        try
        {
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can view user accounts");
            }

            var users = await _accountManagementService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving user accounts" });
        }
    }

    // READ - GET /api/admins/accounts/{userId}
    [HttpGet("accounts/{userId}")]
    public async Task<ActionResult<AccountManagementResponseDTO>> GetUserAccount(int userId)
    {
        try
        {
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can view user accounts");
            }

            if (userId <= 0)
            {
                return BadRequest(new { message = "Invalid user ID" });
            }

            var user = await _accountManagementService.GetUserAccountAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the user account" });
        }
    }

    // READ - GET /api/admins/accounts/type/{userType}
    [HttpGet("accounts/type/{userType}")]
    public async Task<ActionResult<List<AccountManagementResponseDTO>>> GetUsersByType(int userType)
    {
        try
        {
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can view user accounts");
            }

            if (userType < 1 || userType > 3)
            {
                return BadRequest(new { message = "User type must be 1 (Applicant), 2 (Recruiter), or 3 (Admin)" });
            }

            var users = await _accountManagementService.GetUsersByTypeAsync(userType);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving users by type" });
        }
    }

    // UPDATE - PUT /api/admins/accounts/{userId}
    [HttpPut("accounts/{userId}")]
    public async Task<ActionResult<AccountManagementResponseDTO>> UpdateUserAccount(int userId, [FromBody] UpdateUserAccountDTO request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adminUserId = GetUserIdFromToken();
            if (adminUserId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can update user accounts");
            }

            if (userId <= 0)
            {
                return BadRequest(new { message = "Invalid user ID" });
            }

            var result = await _accountManagementService.UpdateUserAccountAsync(userId, request, adminUserId.Value);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the user account" });
        }
    }

    // DELETE - DELETE /api/admins/accounts/{userId}
    [HttpDelete("accounts/{userId}")]
    public async Task<ActionResult> DeleteUserAccount(int userId)
    {
        try
        {
            var adminUserId = GetUserIdFromToken();
            if (adminUserId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can delete user accounts");
            }

            if (userId <= 0)
            {
                return BadRequest(new { message = "Invalid user ID" });
            }

            var result = await _accountManagementService.DeleteUserAccountAsync(userId, adminUserId.Value);
            if (!result)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(new { message = "User account deleted successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the user account" });
        }
    }   

    // GET /api/admins/system/info
    // Gets system information and health status
    [HttpGet("system/info")]
    public async Task<ActionResult<SystemInfoDTO>> GetSystemInfo()
    {
        try
        {
            if (GetUserTypeFromToken() != 3)
            {
                return Forbid("Access denied: Only admins can access this endpoint");
            }

            var systemInfo = new SystemInfoDTO
            {
                ServerTime = DateTime.UtcNow,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                Version = "1.0.0", // You can get this from assembly info
                DatabaseStatus = "Connected", // You could add actual health checks here
                LastRestart = DateTime.UtcNow.AddHours(-1) // Placeholder
            };

            return Ok(systemInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving system info" });
        }
    }

    // Helper methods to extract claims from JWT token
    private int? GetUserIdFromToken()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }
        return null;
    }

    private int? GetUserTypeFromToken()
    {
        var userTypeClaim = User.FindFirst("user_type");
        if (userTypeClaim != null && int.TryParse(userTypeClaim.Value, out int userType))
        {
            return userType;
        }
        return null;
    }
}

// Helper DTOs for admin endpoints
public class AdminDashboardDTO
{
    public int TotalAdmins { get; set; }
    public int TotalApplicants { get; set; }
    public int TotalRecruiters { get; set; }
    public int TotalCompanies { get; set; }
    public DateTime LastUpdated { get; set; }
}

public class ToggleActivationRequest
{
    [Required]
    public bool IsActive { get; set; }
}

public class SystemInfoDTO
{
    public DateTime ServerTime { get; set; }
    public string Environment { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string DatabaseStatus { get; set; } = string.Empty;
    public DateTime LastRestart { get; set; }
}