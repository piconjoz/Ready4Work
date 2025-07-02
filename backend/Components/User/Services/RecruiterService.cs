namespace backend.User.Services;

using backend.User.Models;
using backend.User.DTOs;
using backend.User.Services.Interfaces;
using backend.User.Repositories.Interfaces;

public class RecruiterService : IRecruiterService
{
    private readonly IRecruiterRepository _recruiterRepository;
    private readonly IUserService _userService;
    
    // constructor injection for both recruiter repo & user service
    public RecruiterService(IRecruiterRepository recruiterRepository, IUserService userService)
    {
        _recruiterRepository = recruiterRepository;
        _userService = userService;
    }
    
    // creates a recruiter profile, called during the signup process
    // only creates the recruiter record, but user record must first exist
    public async Task<Recruiter> CreateRecruiterAsync(int userId, int companyId, string jobTitle, string? department)
    {
        // validates that the user exists
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) throw new ArgumentException("User does not exist");
        
        // validates that the user is not already a recruiter
        if (await ExistsByUserIdAsync(userId))
        {
            throw new InvalidOperationException("User already has a recruiter profile");
        }
        
        // validate business rules
        if (companyId <= 0) throw new ArgumentException("Invalid company ID, must be positive");
        if (string.IsNullOrWhiteSpace(jobTitle)) throw new ArgumentException("Job title cannot be empty");
        if (jobTitle.Length > 100) throw new ArgumentException("Job title cannot exceed 100 characters");
        if (department != null && department.Length > 100) throw new ArgumentException("Department cannot exceed 100 characters");

        var recruiter = new Recruiter(userId, companyId, jobTitle, department);
        
        // save the recruiter record to db
        return await _recruiterRepository.CreateAsync(recruiter);
    }
    
    // retrieves a recruiter by their ID
    public async Task<Recruiter?> GetRecruiterByIdAsync(int recruiterId)
    {
        if (recruiterId <= 0) return null;
        return await _recruiterRepository.GetByIdAsync(recruiterId);
    }
    
    // retrieves a recruiter by their user ID
    public async Task<Recruiter?> GetRecruiterByUserIdAsync(int userId)
    {
        if (userId <= 0) return null;
        return await _recruiterRepository.GetByUserIdAsync(userId);
    }
    
    // updates a recruiter's details
    public async Task<Recruiter> UpdateRecruiterDetailsAsync(int recruiterId, int companyId, string jobTitle, string? department)
    {
        var recruiter = await GetRecruiterByIdAsync(recruiterId);
        if (recruiter == null) throw new ArgumentException("Recruiter does not exist");
        
        // validate business rules
        if (companyId <= 0) throw new ArgumentException("Invalid company ID, must be positive");
        if (string.IsNullOrWhiteSpace(jobTitle)) throw new ArgumentException("Job title cannot be empty");
        if (jobTitle.Length > 100) throw new ArgumentException("Job title cannot exceed 100 characters");
        if (department != null && department.Length > 100) throw new ArgumentException("Department cannot exceed 100 characters");
        
        // update using setter methods
        recruiter.SetCompanyId(companyId);
        recruiter.SetJobTitle(jobTitle);
        recruiter.SetDepartment(department);
        recruiter.SetUpdatedAt(DateTime.UtcNow);
        
        return await _recruiterRepository.UpdateAsync(recruiter);
    }
    
    // deletes a recruiter profile
    public async Task<bool> DeleteRecruiterAsync(int recruiterId)
    {
        if (recruiterId <= 0) return false;
        return await _recruiterRepository.DeleteAsync(recruiterId);
    }

    // checks if user has recruiter profile
    public async Task<bool> ExistsByUserIdAsync(int userId)
    {
        if (userId <= 0) return false;
        return await _recruiterRepository.ExistsByUserIdAsync(userId);
    }
    
    // gets all recruiters for a company - used by admin/reports
    public async Task<List<Recruiter>> GetRecruitersByCompanyAsync(int companyId)
    {
        if (companyId <= 0) return new List<Recruiter>();
        return await _recruiterRepository.GetByCompanyIdAsync(companyId);
    }
    
    // gets all recruiters in a department - used by admin/reports
    public async Task<List<Recruiter>> GetRecruitersByDepartmentAsync(string department)
    {
        if (string.IsNullOrWhiteSpace(department)) return new List<Recruiter>();
        return await _recruiterRepository.GetByDepartmentAsync(department);
    }
    
    // converts to DTO w/ user data
    // combines recruiter data with user profile for frontend
    public async Task<RecruiterResponseDTO> ConvertToResponseDTOAsync(Recruiter recruiter)
    {
        var user = await _userService.GetUserByIdAsync(recruiter.GetUserId());
        
        return new RecruiterResponseDTO
        {
            RecruiterId = recruiter.GetRecruiterId(),
            UserId = recruiter.GetUserId(),
            CompanyId = recruiter.GetCompanyId(),
            JobTitle = recruiter.GetJobTitle(),
            Department = recruiter.GetDepartment(),
            CreatedAt = recruiter.GetCreatedAt(),
            UpdatedAt = recruiter.GetUpdatedAt(),
            User = user != null ? _userService.ConvertToResponseDTO(user) : null
        };
    }
    
    // gets complete recruiter profile with user data - used by controllers
    // this is what authenticated users call to get their profile
    public async Task<RecruiterResponseDTO> GetRecruiterProfileAsync(int userId)
    {
        var recruiter = await GetRecruiterByUserIdAsync(userId);
        if (recruiter == null) return null;

        return await ConvertToResponseDTOAsync(recruiter);
    }
}