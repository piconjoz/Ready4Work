using backend.Components.User.Services.Interfaces;

namespace backend.User.Services;

using backend.User.DTOs;
using backend.User.Services.Interfaces;
using backend.Components.Company.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IApplicantService _applicantService;
    private readonly IRecruiterService _recruiterService;
    private readonly IJWTService _jwtService;
    private readonly ICompanyService _companyService;
    private readonly IPasswordService _passwordService;
    private readonly IRefreshTokenService _refreshTokenService;

    // constructor injection - authservice needs ALL other services
    public AuthService(
        IUserService userService,
        IApplicantService applicantService,
        IRecruiterService recruiterService,
        IJWTService jwtService,
        ICompanyService companyService,
        IPasswordService passwordService,
        IRefreshTokenService refreshTokenService
    )
    {
        _userService = userService;
        _applicantService = applicantService;
        _recruiterService = recruiterService;
        _jwtService = jwtService;
        _companyService = companyService;
        _passwordService = passwordService;
        _refreshTokenService = refreshTokenService;
    }

    // handles complete applicant signup flow
    // creates user + applicant profile + returns jwt token + refresh token
    public async Task<AuthResponseDTO> SignupApplicantAsync(ApplicantSignupDTO signupDto)
    {
        // validate input dto (additional business validation)
        if (signupDto == null) throw new ArgumentNullException(nameof(signupDto));

        // step 1: create user record (usertype = 1 for applicant)
        var user = await _userService.CreateUserAsync(
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

        // step 3: revoke any existing tokens (safety measure for new users)
        await _refreshTokenService.RevokeAllUserTokensAsync(user.GetUserId());

        // step 4: generate tokens
        var accessToken = _jwtService.GenerateToken(
            user.GetUserId(),
            user.GetUserType(),
            applicant.GetApplicantId()
        );

        Console.WriteLine("DEBUG: Access token generated");

        var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.GetUserId());

        Console.WriteLine("DEBUG: Refresh token generated");


        // step 5: attach applicantId
        var userDto = _userService.ConvertToResponseDTO(user);
        userDto.ApplicantId = applicant.GetApplicantId();

        Console.WriteLine("DEBUG: User DTO created with ApplicantId");
        
        // step 6: return
        return new AuthResponseDTO
        {
            Token = accessToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15),
            User = userDto,
            RefreshToken = refreshToken
        };
        
    }

    // handles complete recruiter signup flow
    // creates user + recruiter profile + returns jwt token + refresh token
    public async Task<AuthResponseDTO> SignupRecruiterAsync(RecruiterSignupDTO signupDto)
    {
        // validate input dto
        if (signupDto == null) throw new ArgumentNullException(nameof(signupDto));

        // step 1: create user record (usertype = 2 for recruiter)
        var user = await _userService.CreateUserAsync(
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

        // step 3: revoke any existing tokens (safety measure for new users)
        await _refreshTokenService.RevokeAllUserTokensAsync(user.GetUserId());

        // step 4: generate both access and refresh tokens
        var accessToken = _jwtService.GenerateToken(user.GetUserId(), user.GetUserType());
        var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.GetUserId());

        // step 5: return complete auth response
        return new AuthResponseDTO
        {
            Token = accessToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15), // reduced to 15 minutes
            User = _userService.ConvertToResponseDTO(user),
            RefreshToken = refreshToken
        };
    }

    // handles login for all user types (applicant, recruiter, admin)
    // validates credentials and returns jwt token + refresh token
    public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
    {
        Console.WriteLine("DEBUG: Login method started");
    
        // validate input dto
        if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
    
        Console.WriteLine($"DEBUG: Attempting to authenticate user: {loginDto.Email}");
    
        // step 1: authenticate user credentials
        var user = await _userService.AuthenticateUserAsync(loginDto.Email, loginDto.Password);
        if (user == null)
        {
            Console.WriteLine("DEBUG: User authentication failed");
            throw new UnauthorizedAccessException("Invalid email or password");
        }
    
        Console.WriteLine($"DEBUG: User authenticated successfully, UserID: {user.GetUserId()}");
    
        // step 2: check if user account is active and verified
        if (!user.GetIsActive())
        {
            Console.WriteLine("DEBUG: User account is not active");
            throw new UnauthorizedAccessException("Account is deactivated");
        }
    
        Console.WriteLine("DEBUG: About to revoke existing tokens");
    
        // step 3: revoke all existing refresh tokens for this user (single session enforcement)
        await _refreshTokenService.RevokeAllUserTokensAsync(user.GetUserId());
    
        Console.WriteLine("DEBUG: Tokens revoked, generating new tokens");
    
        var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.GetUserId());

        Console.WriteLine("DEBUG: Login completed successfully");

        // step 5: build User DTO and attach applicantId if the user is an applicant
        var userDto = _userService.ConvertToResponseDTO(user);

        if (user.GetUserType() == 1) // 1 = applicant
        {
            var applicantEntity = await _applicantService.GetApplicantByUserIdAsync(user.GetUserId());
            if (applicantEntity != null)
            {
                userDto.ApplicantId = applicantEntity.GetApplicantId();
            }
        }

        // step 6: send response
        // regenerate access token including applicantId if present
        int? applicantId = userDto.ApplicantId;
        var accessToken = _jwtService.GenerateToken(
            user.GetUserId(),
            user.GetUserType(),
            applicantId
        );

        return new AuthResponseDTO
        {
            Token        = accessToken,
            ExpiresAt    = DateTime.UtcNow.AddMinutes(15),
            User         = userDto,
            RefreshToken = refreshToken
        };
    }

    // refreshes an expired token using refresh token (enhanced security)
    public async Task<AuthResponseDTO?> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken)) return null;

        try
        {
            // step 1: rotate the refresh token (validates old one and creates new one)
            var newRefreshToken = await _refreshTokenService.RotateRefreshTokenAsync(refreshToken);

            // step 2: get user ID from the validated token
            var userId = await _refreshTokenService.GetUserIdFromTokenAsync(newRefreshToken);
            if (userId == null) return null;

            // step 3: get user from database to ensure they still exist and are active
            var user = await _userService.GetUserByIdAsync(userId.Value);
            if (user == null || !user.GetIsActive()) return null;

            // step 4: generate new access token
            var newAccessToken = _jwtService.GenerateToken(user.GetUserId(), user.GetUserType());

            return new AuthResponseDTO
            {
                Token = newAccessToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                User = _userService.ConvertToResponseDTO(user),
                RefreshToken = newRefreshToken
            };
        }
        catch
        {
            return null; // invalid refresh token
        }
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

    // revokes a refresh token (for logout)
    public async Task RevokeRefreshTokenAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return;

        await _refreshTokenService.RevokeRefreshTokenAsync(token);
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

    // onboards recruiter and company in one step
    public async Task<RecruiterOnboardingResponseDTO?> OnboardRecruiterAndCompanyAsync(RecruiterOnboardingDTO dto)
    {
        try
        {
            // 1. Check if the company already exists by UEN
            var company = await _companyService.GetCompanyByUENAsync(dto.Company.UEN);

            if (company != null)
            {
                // Company already exists, return error message
                throw new InvalidOperationException("A company with this UEN already exists.");
            }

            // 2. If not, map the DTO to a new Company entity
            var newCompany = new backend.Components.Company.Models.Company
            {
                CompanyName = dto.Company.CompanyName,
                PreferredCompanyName = dto.Company.PreferredCompanyName,
                CompanyDescription = dto.Company.CompanyDescription,
                CountryOfBusinessRegistration = dto.Company.CountryOfBusinessRegistration,
                UEN = dto.Company.UEN,
                NumberOfEmployees = dto.Company.NumberOfEmployees,
                IndustryCluster = dto.Company.IndustryCluster,
                EntityType = dto.Company.EntityType,
                AuthorisedTrainingOrganisation = dto.Company.AuthorisedTrainingOrganisation,
                CompanyWebsite = dto.Company.CompanyWebsite,
                CompanyContact = dto.Company.CompanyContact,
                City = dto.Company.City,
                State = dto.Company.State,
                ZoneLocation = dto.Company.ZoneLocation,
                CountryCode = dto.Company.CountryCode,
                UnitNumber = dto.Company.UnitNumber,
                Floor = dto.Company.Floor,
                AreaCode = dto.Company.AreaCode,
                Block = dto.Company.Block,
                PostalCode = dto.Company.PostalCode,
                EmploymentType = dto.Company.EmploymentType
            };

            // 3. Create the company in the database
            company = await _companyService.CreateCompanyAsync(newCompany);

            // 4. Check if user already exists by email
            var existingUser = await _userService.GetUserByEmailAsync(dto.User.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            // 5. Create the user
            var user = await _userService.CreateUserAsync(
                email: dto.User.Email,
                firstName: dto.User.FirstName,
                lastName: dto.User.LastName,
                phone: dto.User.Phone,
                gender: dto.User.Gender,
                userType: dto.User.UserType,
                password: dto.User.Password
            );

            // 6. Create the recruiter
            var recruiter = await _recruiterService.CreateRecruiterAsync(
                user.GetUserId(),
                company.CompanyId,
                dto.Recruiter.JobTitle,
                dto.Recruiter.Department
            );

            // Return a structured response DTO
            return new RecruiterOnboardingResponseDTO
            {
                Email = user.GetEmail()
            };
        }
        catch (InvalidOperationException ex)
        {
            // Use InvalidOperationException for known business errors
            throw;
        }
        catch (Exception ex)
        {
            // Optionally log the error
            Console.WriteLine($"Error in OnboardRecruiterAndCompanyAsync: {ex.Message}");
            return null;
        }
    }
}