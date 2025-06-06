namespace backend.User.Services;

using backend.User.DTOs;
using backend.User.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IApplicantService _applicantService;
    private readonly IRecruiterService _recruiterService;
    private readonly IJWTService _jwtService;
    
    // constructor injection - authservice needs ALL other services
    public AuthService(
        IUserService userService,
        IApplicantService applicantService,
        IRecruiterService recruiterService,
        IJWTService jwtService)
    {
        _userService = userService;
        _applicantService = applicantService;
        _recruiterService = recruiterService;
        _jwtService = jwtService;
    }
    
    // handles complete applicant signup flow
    // creates user + applicant profile + returns jwt token
    public async Task<AuthResponseDTO> SignupApplicantAsync(ApplicantSignupDTO signupDto)
    {
        // validate input dto (additional business validation)
        if (signupDto == null) throw new ArgumentNullException(nameof(signupDto));
        
        // step 1: create user record (usertype = 1 for applicant)
        var user = await _userService.CreateUserAsync(
            signupDto.NRIC,
            signupDto.Email,
            signupDto.FirstName,
            signupDto.LastName,
            signupDto.Phone,
            signupDto.Gender,
            1, // applicant user type
            signupDto.Password
        );
        
        // step 2: create applicant profile linked to user
        var applicant = await _applicantService.CreateApplicantAsync(
            user.GetUserId(),
            signupDto.ProgrammeId,
            signupDto.AdmitYear
        );
        
        // step 3: generate jwt token for immediate login
        var token = _jwtService.GenerateToken(user.GetUserId(), user.GetUserType());
        
        // step 4: return complete auth response
        return new AuthResponseDTO
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60), // match jwt expiration
            User = _userService.ConvertToResponseDTO(user)
        };
    }
    
    // handles complete recruiter signup flow
    // creates user + recruiter profile + returns jwt token
    public async Task<AuthResponseDTO> SignupRecruiterAsync(RecruiterSignupDTO signupDto)
    {
        // validate input dto
        if (signupDto == null) throw new ArgumentNullException(nameof(signupDto));
        
        // step 1: create user record (usertype = 2 for recruiter)
        var user = await _userService.CreateUserAsync(
            signupDto.NRIC,
            signupDto.Email,
            signupDto.FirstName,
            signupDto.LastName,
            signupDto.Phone,
            signupDto.Gender,
            2, // recruiter user type
            signupDto.Password
        );
        
        // step 2: create recruiter profile linked to user
        var recruiter = await _recruiterService.CreateRecruiterAsync(
            user.GetUserId(),
            signupDto.CompanyId,
            signupDto.JobTitle,
            signupDto.Department
        );
        
        // step 3: generate jwt token for immediate login
        var token = _jwtService.GenerateToken(user.GetUserId(), user.GetUserType());
        
        // step 4: return complete auth response
        return new AuthResponseDTO
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60), // match jwt expiration
            User = _userService.ConvertToResponseDTO(user)
        };
    }
    
    // handles login for all user types (applicant, recruiter, admin)
    // validates credentials and returns jwt token
    public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
    {
        // validate input dto
        if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
        
        // step 1: authenticate user credentials
        var user = await _userService.AuthenticateUserAsync(loginDto.Email, loginDto.Password);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }
        
        // step 2: check if user account is active and verified
        if (!user.GetIsActive())
        {
            throw new UnauthorizedAccessException("Account is deactivated");
        }
        
        // note: we could add email verification check here if needed
        // if (!user.GetIsVerified()) throw new UnauthorizedAccessException("Email not verified");
        
        // step 3: generate jwt token
        var token = _jwtService.GenerateToken(user.GetUserId(), user.GetUserType());
        
        // step 4: return auth response
        return new AuthResponseDTO
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60), // match jwt expiration
            User = _userService.ConvertToResponseDTO(user)
        };
    }
    
    // refreshes an expired token (optional feature for enhanced security)
    public async Task<AuthResponseDTO?> RefreshTokenAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return null;
        
        // get user id from the token (even if expired)
        var userId = _jwtService.GetUserIdFromToken(token);
        if (userId == null) return null;
        
        // get user from database to ensure they still exist and are active
        var user = await _userService.GetUserByIdAsync(userId.Value);
        if (user == null || !user.GetIsActive()) return null;
        
        // generate new token
        var newToken = _jwtService.GenerateToken(user.GetUserId(), user.GetUserType());
        
        return new AuthResponseDTO
        {
            Token = newToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60),
            User = _userService.ConvertToResponseDTO(user)
        };
    }
    
    // validates if a token is still valid
    public async Task<bool> ValidateTokenAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return false;
        
        // check if token is structurally valid and not expired
        if (_jwtService.IsTokenExpired(token)) return false;
        
        // get user from token
        var userId = _jwtService.GetUserIdFromToken(token);
        if (userId == null) return false;
        
        // verify user still exists and is active
        var user = await _userService.GetUserByIdAsync(userId.Value);
        return user != null && user.GetIsActive();
    }
    
    // verifies a user account (for email verification workflow)
    public async Task<bool> VerifyUserAsync(int userId)
    {
        try
        {
            await _userService.VerifyUserAsync(userId);
            return true;
        }
        catch
        {
            return false;
        }
    }
}