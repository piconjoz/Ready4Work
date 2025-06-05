namespace backend.Users.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Recruiter
{
   [Key]
   [Column("recruiter_id")]
   private int RecruiterId { get; set; }
   
   [Required]
   [Column("company_id")]
   private int CompanyId { get; set; }
   
   [Required]
   [Column("user_id")]
   private int UserId { get; set; }
   
   [Required]
   [Column("job_title")]
   [StringLength(100)]
   private string JobTitle { get; set; } = string.Empty;
   
   [Column("department")]
   [StringLength(100)]
   private string? Department { get; set; }
   
   [Column("created_at")]
   private DateTime CreatedAt { get; set; } = DateTime.UtcNow;
   
   [Column("updated_at")]
   private DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
   
   // private constructor for ef core
   private Recruiter() {}
   
   // internal constructor for services
   internal Recruiter(int userId, int companyId, string jobTitle, string? department)
   {
      UserId = userId;
      CompanyId = companyId;
      JobTitle = jobTitle;
      Department = department;
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
   }
   
   // internal getter methods
   internal int GetRecruiterId() => RecruiterId;
   internal int GetCompanyId() => CompanyId;
   internal int GetUserId() => UserId;
   internal string GetJobTitle() => JobTitle;
   internal string? GetDepartment() => Department;
   internal DateTime GetCreatedAt() => CreatedAt;
   internal DateTime GetUpdatedAt() => UpdatedAt;
   
   // internal setter methods
   internal void SetRecruiterId(int recruiterId) => RecruiterId = recruiterId;
   internal void SetCompanyId(int companyId) => CompanyId = companyId;
   internal void SetJobTitle(string jobTitle) => JobTitle = jobTitle;
   internal void SetDepartment(string? department) => Department = department;
   internal void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
}