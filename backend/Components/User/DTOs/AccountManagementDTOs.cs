using System.ComponentModel.DataAnnotations;

namespace backend.User.DTOs;

// DTO for creating any type of user account (admin use)
public class CreateUserAccountDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string LastName { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
    public string? Phone { get; set; }

    [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters")]
    public string? Gender { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 255 characters")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "User type is required")]
    [Range(1, 3, ErrorMessage = "User type must be 1 (Applicant), 2 (Recruiter), or 3 (Admin)")]
    public int UserType { get; set; }

    // Optional role-specific data
    public ApplicantDataDTO? ApplicantData { get; set; }
    public RecruiterDataDTO? RecruiterData { get; set; }
}

// DTO for updating user accounts
public class UpdateUserAccountDTO
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string LastName { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
    public string? Phone { get; set; }

    [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters")]
    public string? Gender { get; set; }

    public bool IsActive { get; set; } = true;
    public bool IsVerified { get; set; } = false;

    // Optional role-specific updates
    public ApplicantDataDTO? ApplicantData { get; set; }
    public RecruiterDataDTO? RecruiterData { get; set; }
}

// Nested DTOs for role-specific data
public class ApplicantDataDTO
{
    [Required(ErrorMessage = "Programme ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Programme ID must be a positive number")]
    public int ProgrammeId { get; set; }

    [Required(ErrorMessage = "Admit year is required")]
    [Range(2020, 2030, ErrorMessage = "Admit year must be between 2020 and 2030")]
    public int AdmitYear { get; set; }
}

public class RecruiterDataDTO
{
    [Required(ErrorMessage = "Company ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Company ID must be a positive number")]
    public int CompanyId { get; set; }

    [Required(ErrorMessage = "Job title is required")]
    [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
    public string JobTitle { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
    public string? Department { get; set; }
}

// Response DTO for account operations
public class AccountManagementResponseDTO
{
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int UserType { get; set; }
    public string UserTypeDisplay { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Role-specific data
    public object? RoleData { get; set; }
}