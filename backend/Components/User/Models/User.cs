using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.User.Models
{
    public class User
    {
        [Key]
        [Column("user_id")]
        private int UserId { get; set; }

        [Required]
        [Column("nric")]
        [StringLength(20)]
        private string NRIC { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        [StringLength(255)]
        private string Email { get; set; } = string.Empty;

        [Required]
        [Column("first_name")]
        [StringLength(100)]
        private string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("last_name")]
        [StringLength(100)]
        private string LastName { get; set; } = string.Empty;

        [Column("phone")]
        [StringLength(20)]
        private string? Phone { get; set; }

        [Column("gender")] 
        [StringLength(10)] // its 2025 broskis, you can be a helicopter if you want to
        private string? Gender { get; set; }
        
        [Column("profile_picture_path")]
        [StringLength(255)]
        private string? ProfilePicturePath { get; set; } = null;

        [Column("is_active")]
        private bool IsActive { get; set; } = true;

        [Column("created_at")]
        private DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        private DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("is_verified")]
        private bool IsVerified { get; set; } = false;

        [Column("salt")]
        [StringLength(255)]
        private string Salt { get; set; } = string.Empty;

        [Column("password_hash")]
        [StringLength(255)]
        private string PasswordHash { get; set; } = string.Empty;

        [Required]
        [Column("user_type")]
        private int UserType { get; set; } = 1; // 1=applicant, 2=recruiter, 3=admin

        // private constructor for ef core
        private User() { }

        // internal constructor - only services can create users
        internal User(string nric, string email, string firstName, string lastName, string? phone, string? gender,
             int userType, string salt, string passwordHash)
        {
            NRIC = nric;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Gender = gender;
            UserType = userType;
            Salt = salt;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        // internal getter methods - only services can read data
        internal int GetUserId() => UserId;
        internal string GetNRIC() => NRIC;
        internal string GetEmail() => Email;
        internal string GetFirstName() => FirstName;
        internal string GetLastName() => LastName;
        internal string? GetPhone() => Phone;
        internal string? GetGender() => Gender;
        internal bool GetIsActive() => IsActive;
        internal DateTime GetCreatedAt() => CreatedAt;
        internal DateTime GetUpdatedAt() => UpdatedAt;
        internal bool GetIsVerified() => IsVerified;
        internal int GetUserType() => UserType;
        internal string? GetProfilePicturePath() => ProfilePicturePath;

        // password methods - only for password service
        internal string GetSalt() => Salt;
        internal string GetPasswordHash() => PasswordHash;

        // internal setter methods - only services can modify
        internal void SetUserId(int userId) => UserId = userId;
        internal void SetFirstName(string firstName) => FirstName = firstName;
        internal void SetLastName(string lastName) => LastName = lastName;
        internal void SetPhone(string? phone) => Phone = phone;
        internal void SetGender(string? gender) => Gender = gender;
        internal void SetIsActive(bool isActive) => IsActive = isActive;
        internal void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
        internal void SetIsVerified(bool isVerified) => IsVerified = isVerified;
        internal void SetSalt(string salt) => Salt = salt;
        internal void SetPasswordHash(string passwordHash) => PasswordHash = passwordHash;
        internal void SetProfilePicturePath(string? profilePicturePath) => ProfilePicturePath = profilePicturePath;
    }
}