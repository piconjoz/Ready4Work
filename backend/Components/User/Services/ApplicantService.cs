namespace backend.User.Services;

using backend.User.Models;
using backend.User.DTOs;
using backend.User.Services.Interfaces;
using backend.User.Repositories.Interfaces;

public class ApplicantService : IApplicantService
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IUserService _userService;
    
    // constructor injection for both applicant repo & user service
    public ApplicantService(IApplicantRepository applicantRepository, IUserService userService)
    {
        _applicantRepository = applicantRepository;
        _userService = userService;
    }
    
    // creates an applicant profile, called during the signup process
    // only creates the applicant record, but user record must first exist
    public async Task<Applicant> CreateApplicantAsync(int userId, int programmeId, int admitYear)
    {
        // validates that the user exists
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null) throw new ArgumentException("User does not exist");
        
        // validates that the user is not already an applicant
        if (await ExistsByUserIdAsync(userId))
        {
            throw new InvalidOperationException("User already has an applicant profile");
        }
        
        if (programmeId <= 0) throw new ArgumentException("Invalid programme ID, must be positive");
        if (admitYear < 2009 || admitYear > DateTime.Now.Year + 1) throw new ArgumentException("Invalid admit year, must be between 2009 and next year");

        var applicant = new Applicant(userId, programmeId, admitYear);
        
        // save the applicant record to db
        return await _applicantRepository.CreateAsync(applicant);
    }
    
    // retrieves an applicant by their ID
    public async Task<Applicant?> GetApplicantByIdAsync(int applicantId)
    {
        if (applicantId <= 0) return null;

        return await _applicantRepository.GetByIdAsync(applicantId);
    }
    
    // retrieves an applicant by their user ID
    public async Task<Applicant?> GetApplicantByUserIdAsync(int userId)
    {
        if (userId <= 0) return null;

        return await _applicantRepository.GetByUserIdAsync(userId);
    }
    
    // updates an applicant's details
    public async Task<Applicant> UpdateApplicantDetailsAsync(int applicantId, int programmeId, int admitYear)
    {
        var applicant = await GetApplicantByIdAsync(applicantId);
        if (applicant == null) throw new ArgumentException("Applicant does not exist");
        
        // validate business rules
        if (programmeId <= 0) throw new ArgumentException("Invalid programme ID, must be positive");
        if (admitYear < 2020 || admitYear > DateTime.Now.Year + 1) throw new ArgumentException("Invalid admit year, must be between 2020 and next year");
        
        // update the setters methods
        applicant.SetProgrammeId(programmeId);
        applicant.SetAdmitYear(admitYear);
        applicant.SetUpdatedAt(DateTime.UtcNow);
        
        return await _applicantRepository.UpdateAsync(applicant);
    }
    
    // deletes an applicant profile
    public async Task<bool> DeleteApplicantAsync(int applicantId)
    {
        if (applicantId <= 0) return false;

        return await _applicantRepository.DeleteAsync(applicantId);
    }

    // checks if user has applicant profile
    public async Task<bool> ExistsByUserIdAsync(int userId)
    {
        if (userId <= 0) return false;
        return await _applicantRepository.ExistsByUserIdAsync(userId);
    }
    
    // gets all applicants for a programme - used by admin/reports
    public async Task<List<Applicant>> GetApplicantsByProgrammeAsync(int programmeId)
    {
        if (programmeId <= 0) return new List<Applicant>();

        return await _applicantRepository.GetByProgrammeIdAsync(programmeId);
    }
    
    // gets all applicants for a year - used by admin/reports
    public async Task<List<Applicant>> GetApplicantsByYearAsync(int admitYear)
    {
        if (admitYear < 2020 || admitYear > DateTime.Now.Year + 1) return new List<Applicant>();
        
        return await _applicantRepository.GetByAdmitYearAsync(admitYear);
    }
    
    // converts to DTO w/ user data
    // combines applicant data with user profile for frontend
    public async Task<ApplicantResponseDTO> ConvertToResponseDTOAsync(Applicant applicant)
    {
        var user = await _userService.GetUserByIdAsync(applicant.GetUserId());
        
        return new ApplicantResponseDTO
        {
            ApplicantId = applicant.GetApplicantId(),
            UserId = applicant.GetUserId(),
            ProgrammeId = applicant.GetProgrammeId(),
            AdmitYear = applicant.GetAdmitYear(),
            CreatedAt = applicant.GetCreatedAt(),
            UpdatedAt = applicant.GetUpdatedAt(),
            User = user != null ? _userService.ConvertToResponseDTO(user) : null
        };
    }
    
    // gets complete applicant profile with user data - used by controllers
    // this is what authenticated users call to get their profile
    public async Task<ApplicantResponseDTO> GetApplicantProfileAsync(int userId)
    {
        var applicant = await GetApplicantByUserIdAsync(userId);
        if (applicant == null) return null;

        return await ConvertToResponseDTOAsync(applicant);
    }
}