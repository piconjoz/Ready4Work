# Security-First Architecture Overview

## **Project Overview**
ASP.NET 8 Web API with React 19 frontend - Security-focused recruitment/application system with three user types: Applicants, Recruiters, and Admins.

## **Architecture Principles**
- **Layered architecture**: Controllers → Services → Repositories → Database
- **Strong encapsulation**: User entities have private methods, only UserService can access User data directly
- **Security paramount**: Minimal public methods, proper OOP principles
- **JWT token authentication** for React SPA
- **Repository pattern** with interfaces for each entity
- **Service composition** when services need data from multiple entities

## **Class Overview by Layer**

### **1. Entities (Data Containers Only) - COMPLETED ✅**
```csharp
User.cs          // user_id, nric, email, first_name, last_name, phone, gender, 
                 // is_active, created_at, updated_at, is_verified, salt, password_hash, Type
                 // ALL PROPERTIES PRIVATE with internal getter methods
                 // Private constructors, internal creation methods only
Applicant.cs     // applicant_id, user_id, programme_id, admit_year, created_at, updated_at
Recruiter.cs     // recruiter_id, company_id, user_id, job_title, department  
Admin.cs         // admin_id, user_id
```

### **Security Services - COMPLETED ✅**
```csharp
IPasswordService.cs & PasswordService.cs // HashPassword, VerifyPassword, GenerateSalt
                                         // PBKDF2 with salt, timing attack protection
```

### **2. DTOs (Input/Output Protection)**
```csharp
// Input DTOs
ApplicantSignupDto.cs    // nric, email, first_name, last_name, phone, gender, password, programme_id, admit_year
RecruiterSignupDto.cs    // nric, email, first_name, last_name, phone, gender, password, company_id, job_title, department
LoginDto.cs              // email, password

// Output DTOs  
UserResponseDto.cs       // user_id, email, first_name, last_name, phone, gender, Type
ApplicantResponseDto.cs  // applicant_id, user data, programme_id, admit_year
AuthResponseDto.cs       // token, user info
```

### **3. Repository Interfaces & Implementations**
```csharp
IUserRepository.cs & UserRepository.cs           // GetById, GetByEmail, Create, Update, Delete
IApplicantRepository.cs & ApplicantRepository.cs // GetById, GetByUserId, Create, Update, Delete  
IRecruiterRepository.cs & RecruiterRepository.cs // GetById, GetByUserId, Create, Update, Delete
IAdminRepository.cs & AdminRepository.cs         // GetById, GetByUserId, Create, Update, Delete
```

### **4. Services (Business Logic & Security)**
```csharp
IPasswordService.cs & PasswordService.cs         // HashPassword, VerifyPassword
IUserService.cs & UserService.cs                 // CreateUser, GetUser, UpdateUser, ValidateUser
IApplicantService.cs & ApplicantService.cs       // CreateApplicant, GetApplicant (uses UserService)
IRecruiterService.cs & RecruiterService.cs       // CreateRecruiter, GetRecruiter (uses UserService)  
IAuthService.cs & AuthService.cs                 // SignupApplicant, SignupRecruiter, Login, GenerateToken
IJwtService.cs & JwtService.cs                   // GenerateToken, ValidateToken
```

### **5. Controllers (Thin Layer)**
```csharp
AuthController.cs        // POST /api/auth/signup/applicant, /api/auth/signup/recruiter, /api/auth/login
UserController.cs        // GET /api/users/profile (authenticated)
ApplicantController.cs   // GET /api/applicants/profile (authenticated applicants only)
```

## **Applicant Signup Flow**

### **Step-by-Step Process:**
1. **Client** → POST `/api/auth/signup/applicant` with ApplicantSignupDto
2. **AuthController** → calls `authService.SignupApplicant(dto)`
3. **AuthService** → validates input, calls `userService.CreateUser(userData)`
4. **UserService** → calls `passwordService.HashPassword()`, calls `userRepository.Create()`
5. **AuthService** → calls `applicantService.CreateApplicant(applicantData, userId)`
6. **ApplicantService** → calls `applicantRepository.Create()`
7. **AuthService** → calls `jwtService.GenerateToken(userId)`
8. **AuthController** → returns AuthResponseDto with token

### **Security Checkpoints:**
- DTO validation at controller level
- Business rule validation in AuthService
- Password hashing in PasswordService
- User creation only through UserService
- JWT contains only user_id for data filtering
- No direct repository access from controllers

### **Data Access Pattern:**
- Need applicant profile? → ApplicantService → UserService → UserRepository
- Need user data? → Only through UserService
- Cross-service communication through service dependencies

## **Key Security Decisions**
- **Separate signup endpoints**: `/api/auth/signup/applicant` and `/api/auth/signup/recruiter` to prevent userType manipulation
- **Password hashing** via separate IPasswordService/PasswordService using PBKDF2 with salt
- **JWT tokens contain user_id** for data filtering - users only access their own records
- **DTOs for input/output** validation and preventing over-posting attacks
- **Admin creation** only through admin-restricted endpoints
- **Entity relationships**: Option 3 - Neither entity knows about each other (services handle relationships)
- **Repository access**: Only through corresponding services
- **Business logic**: Lives in services, not entities
- **Maximum encapsulation**: All entity properties are private with internal getter methods (Option A)
- **Private constructors**: Only services can create entity instances
- **Internal methods only**: Only services can modify entity state

## **Database Structure**
- **Users table**: user_id (PK), nric, email, first_name, last_name, phone, gender, is_active, created_at, updated_at, is_verified, salt, log, Type
- **Applicant table**: applicant_id (PK), user_id (FK), programme_id (FK), admit_year, created_at, updated_at
- **Recruiter table**: recruiter_id (PK), company_id (FK), user_id (FK), job_title, department
- **Admin table**: admin_id (PK), user_id (FK)
- **One-to-one relationships** between Users and specific role tables

## **Implementation Priority**
1. Entities (simple data containers)
2. DTOs (input/output validation)
3. Repository interfaces and implementations
4. Core services (Password, User, JWT)
5. Role services (Applicant, Recruiter, Admin)
6. Auth service (orchestrates signup/login)
7. Controllers (thin layer)
8. Middleware and authentication setup