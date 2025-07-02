## Authentication Flow Testing

### Test 1: Applicant Registration

**Endpoint:** `POST /api/auth/signup/applicant`

**Request Body:**
```json
{
  "nric": "S1234567A",
  "email": "john.doe@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "phone": "98765432",
  "gender": "Male",
  "password": "SecurePassword123",
  "programmeId": 1,
  "admitYear": 2025
}
```

**Expected Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-01-07T12:30:00Z",
  "user": {
    "userId": 1,
    "nric": "S1234567A",
    "email": "john.doe@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "phone": "98765432",
    "gender": "Male",
    "profilePicturePath": null,
    "isActive": true,
    "createdAt": "2025-01-06T11:30:00Z",
    "updatedAt": "2025-01-06T11:30:00Z",
    "isVerified": false,
    "userType": 1
  }
}
```

**What Happens Behind the Scenes:**

1. **AuthController.SignupApplicant()** receives the request
    - **Location**: `Controllers/AuthController.cs:15`
    - **Validation**: Uses `[Required]`, `[EmailAddress]` attributes from `ApplicantSignupDTO`

2. **AuthService.SignupApplicantAsync()** orchestrates the process
    - **Location**: `Services/AuthService.cs:25`
    - **Step 1**: Creates user with `userType = 1` (hardcoded for security)
    - **Step 2**: Creates applicant profile linked to user
    - **Step 3**: Generates JWT token

3. **UserService.CreateUserAsync()** handles user creation
    - **Location**: `Services/UserService.cs:18`
    - **Security Check**: Validates email/NRIC uniqueness
    - **Password Hashing**: Uses `PasswordService.HashPassword()` with PBKDF2

4. **ApplicantService.CreateApplicantAsync()** creates applicant profile
    - **Location**: `Services/ApplicantService.cs:18`
    - **Security Check**: Ensures user exists before creating profile
    - **Business Logic**: Validates programme ID and admit year ranges

5. **JWTService.GenerateToken()** creates authentication token
    - **Location**: `Services/JWTService.cs:25`
    - **Claims Added**: User ID, User Type (1), JTI, Issue Time
    - **Security**: HMAC SHA256 signing with your secret key

### Test 2: Recruiter Registration

**Endpoint:** `POST /api/auth/signup/recruiter`

**Request Body:**
```json
{
  "nric": "S7654321B",
  "email": "jane.smith@techcorp.com",
  "firstName": "Jane",
  "lastName": "Smith",
  "phone": "87654321",
  "gender": "Female",
  "password": "AnotherSecurePassword456",
  "companyId": 1,
  "jobTitle": "Senior Software Engineer",
  "department": "Engineering"
}
```

**Expected Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-01-07T12:35:00Z",
  "user": {
    "userId": 2,
    "nric": "S7654321B",
    "email": "jane.smith@techcorp.com",
    "firstName": "Jane",
    "lastName": "Smith",
    "phone": "87654321",
    "gender": "Female",
    "profilePicturePath": null,
    "isActive": true,
    "createdAt": "2025-01-06T11:35:00Z",
    "updatedAt": "2025-01-06T11:35:00Z",
    "isVerified": false,
    "userType": 2
  }
}
```

**Key Differences from Applicant Signup:**
- **UserType**: Set to `2` (recruiter) in `AuthService.SignupRecruiterAsync()`
- **Profile Creation**: Uses `RecruiterService.CreateRecruiterAsync()`
- **Validation**: Company ID, job title validation instead of programme/year

### Test 3: Login (Works for All User Types)

**Endpoint:** `POST /api/auth/login`

**Request Body:**
```json
{
  "email": "john.doe@example.com",
  "password": "SecurePassword123"
}
```

**Expected Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-01-07T12:40:00Z",
  "user": {
    "userId": 1,
    "nric": "S1234567A",
    "email": "john.doe@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "phone": "98765432",
    "gender": "Male",
    "profilePicturePath": null,
    "isActive": true,
    "createdAt": "2025-01-06T11:30:00Z",
    "updatedAt": "2025-01-06T11:30:00Z",
    "isVerified": false,
    "userType": 1
  }
}
```

**Authentication Process:**

1. **AuthController.Login()** validates input
    - **Location**: `Controllers/AuthController.cs:85`
    - **DTO Validation**: `LoginDTO` with email/password requirements

2. **AuthService.LoginAsync()** handles authentication
    - **Location**: `Services/AuthService.cs:75`
    - **Step 1**: Calls `UserService.AuthenticateUserAsync()`
    - **Step 2**: Validates user is active
    - **Step 3**: Generates new JWT token

3. **UserService.AuthenticateUserAsync()** verifies credentials
    - **Location**: `Services/UserService.cs:70`
    - **Security**: Uses `PasswordService.VerifyPassword()` with constant-time comparison
    - **Password Check**: Hashes input password with stored salt, compares hashes

---

## User Profile Management

### Test 4: Get User Profile (Requires Authentication)

**Endpoint:** `GET /api/user/profile`

**Headers Required:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Expected Response (200 OK):**
```json
{
  "userId": 1,
  "nric": "S1234567A",
  "email": "john.doe@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "phone": "98765432",
  "gender": "Male",
  "profilePicturePath": null,
  "isActive": true,
  "createdAt": "2025-01-06T11:30:00Z",
  "updatedAt": "2025-01-06T11:30:00Z",
  "isVerified": false,
  "userType": 1
}
```

**Security Implementation:**

1. **JWT Middleware** validates token automatically
    - **Location**: `Program.cs:49` - `app.UseAuthentication()`
    - **Validation**: Checks signature, expiration, issuer, audience
    - **Claims Extraction**: Populates `HttpContext.User` with claims

2. **UserController.GetProfile()** extracts user ID from token
    - **Location**: `Controllers/UserController.cs:20`
    - **Security**: `[Authorize]` attribute requires valid JWT
    - **User Isolation**: `GetUserIdFromToken()` ensures users only see their own data

3. **UserService.GetUserByIdAsync()** retrieves user data
    - **Location**: `Services/UserService.cs:46`
    - **Entity Security**: Uses entity getter methods to access private properties

### Test 5: Update User Profile

**Endpoint:** `PUT /api/user/profile`

**Headers Required:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Request Body:**
```json
{
  "firstName": "Jonathan",
  "lastName": "Doe-Smith",
  "phone": "91234567",
  "gender": "Male"
}
```

**Expected Response (200 OK):**
```json
{
  "userId": 1,
  "nric": "S1234567A",
  "email": "john.doe@example.com",
  "firstName": "Jonathan",
  "lastName": "Doe-Smith",
  "phone": "91234567",
  "gender": "Male",
  "profilePicturePath": null,
  "isActive": true,
  "createdAt": "2025-01-06T11:30:00Z",
  "updatedAt": "2025-01-06T11:45:00Z",
  "isVerified": false,
  "userType": 1
}
```

**Update Process:**

1. **Input Validation**: `UpdateProfileRequest` DTO validates data
2. **UserService.UpdateUserDetailsAsync()** modifies user
    - **Location**: `Services/UserService.cs:79`
    - **Entity Security**: Uses entity setter methods (`user.SetFirstName()`)
    - **Audit Trail**: Automatically updates `UpdatedAt` timestamp

### Test 6: Update Password

**Endpoint:** `PUT /api/user/password`

**Headers Required:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Request Body:**
```json
{
  "newPassword": "NewSecurePassword789"
}
```

**Expected Response (200 OK):**
```json
{
  "message": "Password updated successfully"
}
```

**Security Process:**

1. **Password Hashing**: `PasswordService.HashPassword()` generates new salt and hash
2. **Entity Update**: Uses `user.SetSalt()` and `user.SetPasswordHash()`
3. **Database Update**: Saves new credentials securely

---

## Role-Specific Endpoints

### Test 7: Get Applicant Profile (Applicants Only)

**Endpoint:** `GET /api/applicant/profile`

**Headers Required:**
```
Authorization: Bearer [APPLICANT_JWT_TOKEN]
```

**Expected Response (200 OK):**
```json
{
  "applicantId": 1,
  "userId": 1,
  "programmeId": 1,
  "admitYear": 2025,
  "createdAt": "2025-01-06T11:30:00Z",
  "updatedAt": "2025-01-06T11:30:00Z",
  "user": {
    "userId": 1,
    "nric": "S1234567A",
    "email": "john.doe@example.com",
    "firstName": "Jonathan",
    "lastName": "Doe-Smith",
    "phone": "91234567",
    "gender": "Male",
    "profilePicturePath": null,
    "isActive": true,
    "createdAt": "2025-01-06T11:30:00Z",
    "updatedAt": "2025-01-06T11:45:00Z",
    "isVerified": false,
    "userType": 1
  }
}
```

**Role-Based Security:**

1. **ApplicantController.GetProfile()** checks user type
    - **Location**: `Controllers/ApplicantController.cs:20`
    - **Security Check**: `GetUserTypeFromToken() != 1` returns `403 Forbidden`
    - **Token Claims**: Reads `user_type` claim from JWT

2. **ApplicantService.GetApplicantProfileAsync()** combines data
    - **Location**: `Services/ApplicantService.cs:130`
    - **Data Composition**: Gets applicant data + user data via UserService
    - **Service Security**: Only ApplicantService can access Applicant entities

### Test 8: Update Applicant Profile

**Endpoint:** `PUT /api/applicant/profile`

**Headers Required:**
```
Authorization: Bearer [APPLICANT_JWT_TOKEN]
```

**Request Body:**
```json
{
  "programmeId": 2,
  "admitYear": 2026
}
```

**Expected Response (200 OK):**
```json
{
  "applicantId": 1,
  "userId": 1,
  "programmeId": 2,
  "admitYear": 2026,
  "createdAt": "2025-01-06T11:30:00Z",
  "updatedAt": "2025-01-06T12:00:00Z",
  "user": {
    // ... user data
  }
}
```

### Test 9: Get Recruiter Profile (Recruiters Only)

**Endpoint:** `GET /api/recruiter/profile`

**Headers Required:**
```
Authorization: Bearer [RECRUITER_JWT_TOKEN]
```

**Expected Response (200 OK):**
```json
{
  "recruiterId": 1,
  "userId": 2,
  "companyId": 1,
  "jobTitle": "Senior Software Engineer",
  "department": "Engineering",
  "createdAt": "2025-01-06T11:35:00Z",
  "updatedAt": "2025-01-06T11:35:00Z",
  "user": {
    "userId": 2,
    "nric": "S7654321B",
    "email": "jane.smith@techcorp.com",
    "firstName": "Jane",
    "lastName": "Smith",
    "phone": "87654321",
    "gender": "Female",
    "profilePicturePath": null,
    "isActive": true,
    "createdAt": "2025-01-06T11:35:00Z",
    "updatedAt": "2025-01-06T11:35:00Z",
    "isVerified": false,
    "userType": 2
  }
}
```

---

## Advanced Testing Scenarios

### Test 10: Token Validation

**Endpoint:** `POST /api/auth/validate`

**Request Body:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Expected Response (200 OK):**
```json
{
  "isValid": true
}
```

**Validation Process:**

1. **JWTService.IsTokenExpired()** checks expiration
2. **JWTService.GetUserIdFromToken()** extracts user ID
3. **UserService.GetUserByIdAsync()** verifies user still exists and is active

### Test 11: Token Refresh

**Endpoint:** `POST /api/auth/refresh`

**Request Body:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Expected Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...[NEW_TOKEN]",
  "expiresAt": "2025-01-07T13:00:00Z",
  "user": {
    // ... updated user data
  }
}
```

### Test 12: Profile Picture Upload

**Endpoint:** `PUT /api/user/profile-picture`

**Headers Required:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Request Body:**
```json
{
  "profilePicturePath": "/uploads/profile-pics/user1.jpg"
}
```

**Expected Response (200 OK):**
```json
{
  "userId": 1,
  "nric": "S1234567A",
  "email": "john.doe@example.com",
  "firstName": "Jonathan",
  "lastName": "Doe-Smith",
  "phone": "91234567",
  "gender": "Male",
  "profilePicturePath": "/uploads/profile-pics/user1.jpg",
  "isActive": true,
  "createdAt": "2025-01-06T11:30:00Z",
  "updatedAt": "2025-01-06T12:15:00Z",
  "isVerified": false,
  "userType": 1
}
```

---

## Security Testing

### Test 13: Unauthorized Access Attempts

**Test 13a: Access Protected Endpoint Without Token**

**Endpoint:** `GET /api/user/profile`
**Headers:** None

**Expected Response (401 Unauthorized):**
```json
{
  "message": "Unauthorized"
}
```

**Security Mechanism:**
- **JWT Middleware**: Rejects requests without valid `Authorization` header
- **[Authorize] Attribute**: Blocks access to protected controllers

**Test 13b: Access with Invalid Token**

**Endpoint:** `GET /api/user/profile`
**Headers:**
```
Authorization: Bearer invalid.token.here
```

**Expected Response (401 Unauthorized):**
```json
{
  "message": "Unauthorized"
}
```

**Test 13c: Cross-Role Access Attempt**

**Endpoint:** `GET /api/applicant/profile`
**Headers:**
```
Authorization: Bearer [RECRUITER_JWT_TOKEN]
```

**Expected Response (403 Forbidden):**
```
Access denied: Only applicants can access this endpoint
```

**Security Implementation:**
- **ApplicantController**: Checks `GetUserTypeFromToken() != 1`
- **Role Enforcement**: Prevents recruiters from accessing applicant endpoints

### Test 14: Input Validation Testing

**Test 14a: Invalid Email Format**

**Endpoint:** `POST /api/auth/signup/applicant`

**Request Body:**
```json
{
  "nric": "S1234567A",
  "email": "invalid-email",
  "firstName": "John",
  "lastName": "Doe",
  "phone": "98765432",
  "gender": "Male",
  "password": "SecurePassword123",
  "programmeId": 1,
  "admitYear": 2025
}
```

**Expected Response (400 Bad Request):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Email": [
      "invalid email format"
    ]
  }
}
```

**Test 14b: Password Too Short**

**Request Body:**
```json
{
  "nric": "S1234567A",
  "email": "john.doe@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "phone": "98765432",
  "gender": "Male",
  "password": "123",
  "programmeId": 1,
  "admitYear": 2025
}
```

**Expected Response (400 Bad Request):**
```json
{
  "errors": {
    "Password": [
      "password must be between 8 and 255 characters"
    ]
  }
}
```

---

## Error Handling Verification

### Test 15: Duplicate Registration Attempts

**Test 15a: Duplicate Email**

**Endpoint:** `POST /api/auth/signup/applicant`

**Request Body:** (Use same email as Test 1)
```json
{
  "nric": "S9999999Z",
  "email": "john.doe@example.com",
  "firstName": "Different",
  "lastName": "Person",
  "phone": "99999999",
  "gender": "Male",
  "password": "DifferentPassword123",
  "programmeId": 2,
  "admitYear": 2025
}
```

**Expected Response (409 Conflict):**
```json
{
  "message": "email already exists"
}
```

**Error Handling Chain:**
1. **UserService.CreateUserAsync()** calls `EmailExistsAsync()`
2. **UserRepository.ExistsByEmailAsync()** queries database
3. **InvalidOperationException** thrown and caught by controller
4. **AuthController** returns `409 Conflict` status

**Test 15b: Duplicate NRIC**

**Request Body:**
```json
{
  "nric": "S1234567A",
  "email": "different.email@example.com",
  "firstName": "Different",
  "lastName": "Person",
  "phone": "99999999",
  "gender": "Male",
  "password": "DifferentPassword123",
  "programmeId": 2,
  "admitYear": 2025
}
```

**Expected Response (409 Conflict):**
```json
{
  "message": "nric already exists"
}
```

### Test 16: Invalid Login Attempts

**Test 16a: Wrong Password**

**Endpoint:** `POST /api/auth/login`

**Request Body:**
```json
{
  "email": "john.doe@example.com",
  "password": "WrongPassword123"
}
```

**Expected Response (401 Unauthorized):**
```json
{
  "message": "Invalid email or password"
}
```

**Security Process:**
1. **UserService.AuthenticateUserAsync()** calls `ValidateUserCredentialsAsync()`
2. **PasswordService.VerifyPassword()** performs constant-time comparison
3. **Hash mismatch** detected, returns false
4. **UnauthorizedAccessException** thrown and caught by controller

**Test 16b: Non-existent User**

**Request Body:**
```json
{
  "email": "nonexistent@example.com",
  "password": "AnyPassword123"
}
```

**Expected Response (401 Unauthorized):**
```json
{
  "message": "Invalid email or password"
}
```

Theres a few things missing from this implementation
1. Implement BCrypt for password hashing
2. I'm just using basic JWT Authentication, will need to implement refreshing of tokens, storing the refresh/CSRF token in cookies.   

---
