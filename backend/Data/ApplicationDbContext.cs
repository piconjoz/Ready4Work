// backend/Data/ApplicationDbContext.cs
namespace backend.Data;

using Microsoft.EntityFrameworkCore;
using backend.Components.Company.Models;
using backend.Components.Application.Models;
using backend.Components.CoverLetter.Models;
using backend.User.Models;
using backend.Components.Resume.Models;
using backend.Components.Bookmark.Models;
using backend.Components.JobListing.Models;
using backend.Components.User.Models;
using backend.Components.Skill.Models;
using backend.Components.JobScheme.Models;
using backend.Components.RemunerationType.Models;
using backend.Components.Programme.Models;
using backend.Components.Qualification.Models;
using backend.Components.JobSkill.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Recruiter> Recruiters { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<CoverLetter> CoverLetters { get; set; }  // Add CoverLetter DbSet
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<JobListing> JobListings { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Programme> Programmes { get; set; }
    public DbSet<JobSkill> JobSkills { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<JobScheme> JobSchemes { get; set; }
    public DbSet<RemunerationType> RemunerationTypes { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId);
            entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.PreferredCompanyName).HasMaxLength(200);
            entity.Property(e => e.CompanyDescription).HasMaxLength(1000);
            entity.Property(e => e.CountryOfBusinessRegistration).HasMaxLength(100);
            entity.Property(e => e.UEN).HasMaxLength(50);
            entity.Property(e => e.IndustryCluster).HasMaxLength(100);
            entity.Property(e => e.EntityType).HasMaxLength(100);
            entity.Property(e => e.CompanyWebsite).HasMaxLength(500);

            entity.HasIndex(e => e.UEN).IsUnique();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey("UserId");

            entity.Property("Email")
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");

            entity.Property("FirstName")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("first_name");

            entity.Property("LastName")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("last_name");

            entity.Property("Phone")
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.Property("Gender")
                .HasMaxLength(10)
                .HasColumnName("gender");

            entity.Property("ProfilePicturePath")
                .HasMaxLength(255)
                .HasColumnName("profile_picture_path");

            entity.Property("IsActive")
                .HasColumnName("is_active");

            entity.Property("CreatedAt")
                .HasColumnName("created_at");

            entity.Property("UpdatedAt")
                .HasColumnName("updated_at");

            entity.Property("IsVerified")
                .HasColumnName("is_verified");

            entity.Property("Salt")
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("salt");

            entity.Property("PasswordHash")
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("password_hash");

            entity.Property("UserType")
                .IsRequired()
                .HasColumnName("user_type");

            entity.HasIndex("Email").IsUnique();
        });

        // applicant entity
        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.HasKey("ApplicantId");

            entity.Property("UserId")
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property("ProgrammeId")
                .IsRequired()
                .HasColumnName("programme_id");

            entity.Property("AdmitYear")
                .IsRequired()
                .HasColumnName("admit_year");

            entity.Property("CreatedAt")
                .HasColumnName("created_at");

            entity.Property("UpdatedAt")
                .HasColumnName("updated_at");

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex("UserId").IsUnique();
        });

        // recruiter entity
        modelBuilder.Entity<Recruiter>(entity =>
        {
            entity.HasKey("RecruiterId");

            entity.Property("CompanyId")
                .IsRequired()
                .HasColumnName("company_id");

            entity.Property("UserId")
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property("JobTitle")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("job_title");

            entity.Property("Department")
                .HasMaxLength(100)
                .HasColumnName("department");

            entity.Property("CreatedAt")
                .HasColumnName("created_at");

            entity.Property("UpdatedAt")
                .HasColumnName("updated_at");

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Company>()
                .WithMany()
                .HasForeignKey("CompanyId")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex("UserId").IsUnique();
            entity.HasIndex("CompanyId");
        });

        // admin entity
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey("AdminId");

            entity.Property("UserId")
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property("CreatedAt")
                .HasColumnName("created_at");

            entity.Property("UpdatedAt")
                .HasColumnName("updated_at");

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex("UserId").IsUnique();
        });

        // refresh token entity
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey("RefreshTokenId");

            entity.Property("UserId")
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property("Token")
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("token");

            entity.Property("ExpiresAt")
                .IsRequired()
                .HasColumnName("expires_at");

            entity.Property("CreatedAt")
                .HasColumnName("created_at");

            entity.Property("IsRevoked")
                .HasColumnName("is_revoked");

            entity.Property("RevokedAt")
                .HasColumnName("revoked_at");

            // Foreign key relationship to User
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes for performance
            entity.HasIndex("UserId");
            entity.HasIndex("Token").IsUnique();
            entity.HasIndex("ExpiresAt");
        });

        // Configure CoverLetter entity
        modelBuilder.Entity<CoverLetter>(entity =>
        {
            entity.HasKey(e => e.CoverLetterId);
            entity.Property(e => e.CoverLetterId).HasColumnName("cover_letter_id");
            entity.Property(e => e.ApplicantId).IsRequired().HasColumnName("applicant_id");
            entity.Property(e => e.CoverLetterPath).IsRequired().HasMaxLength(1000).HasColumnName("cover_letter_path");
            entity.Property(e => e.OriginalText).HasColumnName("original_text");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.ContentType).HasMaxLength(50).HasColumnName("content_type");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            // Foreign key to Applicant
            entity.HasOne<Applicant>()
                    .WithMany()
                    .HasForeignKey("ApplicantId")
                    .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.ApplicantId);
        });

        // Configure JobApplication entity with updated relationships
        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey("ApplicationId");
            entity.Property("ApplicantId").IsRequired().HasColumnName("applicant_id");
            entity.Property("JobId").IsRequired().HasColumnName("job_id");
            entity.Property("CoverLetterId").IsRequired().HasColumnName("cover_letter_id");
            entity.Property("Status").IsRequired().HasMaxLength(50).HasColumnName("status");
            entity.Property("AppliedDate").HasColumnName("applied_date");
            entity.Property("UpdatedAt").HasColumnName("updated_at");

            // Foreign key to Applicant
            entity.HasOne<Applicant>("Applicant")
                    .WithMany()
                    .HasForeignKey("ApplicantId")
                    .OnDelete(DeleteBehavior.Cascade);

            // Foreign key to CoverLetter
            entity.HasOne<CoverLetter>("CoverLetter")
                    .WithMany()
                    .HasForeignKey("CoverLetterId")
                    .OnDelete(DeleteBehavior.Restrict);  // Don't cascade delete cover letters

            // Foreign Key to JobListing based on jobId
            entity.HasOne<JobListing>()
                    .WithMany()
                    .HasForeignKey("JobId")
                    .OnDelete(DeleteBehavior.Cascade); // deletion of job listing means deleting the applications to it

            entity.HasIndex("ApplicantId", "JobId").IsUnique(); // Prevent duplicate applications
            entity.HasIndex("CoverLetterId");
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.HasKey(e => e.ResumeId);
            entity.Property(e => e.ResumeId).HasColumnName("resume_id");
            entity.Property(e => e.ApplicantId).IsRequired().HasColumnName("applicant_id");
            entity.Property(e => e.ResumePath).IsRequired().HasMaxLength(1000).HasColumnName("resume_path");
            entity.Property(e => e.ResumeText).IsRequired().HasColumnName("resume_text");
            entity.Property(e => e.UploadedAt).IsRequired().HasColumnName("uploaded_at");

            entity.HasOne<Applicant>()
                    .WithMany()
                    .HasForeignKey("ApplicantId")
                    .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.ApplicantId);
        });

        // Configure Bookmark entity
        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.HasKey(e => e.BookmarkId);
            entity.Property(e => e.BookmarkId).HasColumnName("bookmark_id");
            entity.Property(e => e.ApplicantId).IsRequired().HasColumnName("applicant_id");
            entity.Property(e => e.JobsId).IsRequired().HasColumnName("jobs_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.HasIndex(e => new { e.ApplicantId, e.JobsId }).IsUnique();
        });

        // JobListing
        modelBuilder.Entity<JobListing>(entity =>
        {
            entity.HasKey("JobId");
            entity.Property("RecruiterId")
                .IsRequired()
                .HasColumnName("recruiter_id");

            entity.Property("JobRequirements")
                .HasColumnName("job_requirements");

            entity.Property("JobDescription")
                .HasColumnName("job_description");

            entity.Property("ListingName")
                .HasColumnName("listing_name");

            entity.Property("Deadline")
                .HasColumnName("deadline");

            entity.Property("MaxVacancies")
                .HasColumnName("max_vacancies");

            entity.Property("IsVisible")
                .HasColumnName("is_visible");

            entity.Property("RemunerationType")
                .HasColumnName("remuneration_type");

            entity.Property("JobDuration")
                .HasColumnName("job_duration");

            entity.Property("Rate")
                .HasColumnName("rate");

            entity.Property("WorkingHours")
                .HasColumnName("working_hours");

            entity.Property("JobScheme")
                .HasColumnName("job_scheme");

            entity.Property("PermittedQualifications")
                .HasColumnName("permitted_qualifications");

            entity.Property("Skillsets")
                .HasColumnName("skillsets");

            entity.Property("JobStatus")
                .HasColumnName("job_status");

            entity.Property("CreatedAt")
                .HasColumnName("created_at");

            entity.Property("UpdatedAt")
                .HasColumnName("updated_at");

            entity.HasOne<Recruiter>()
                .WithMany()
                .HasForeignKey("RecruiterId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Skills entity
        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey("SkillId");
            entity.Property("skill").HasColumnName("skill");
        });

        // Programme entity
        modelBuilder.Entity<Programme>(entity =>
        {
            entity.HasKey("ProgrammeId");
            entity.Property("ProgrammeName").HasColumnName("programme_name");
        });

        // Job skills entity
        modelBuilder.Entity<JobSkill>(entity =>
        {
            entity.HasKey("JobSkillId");
            entity.Property("JobId").HasColumnName("job_id").IsRequired();
            entity.Property("SkillId").HasColumnName("skill_id").IsRequired();

            entity.HasOne<JobListing>()
                .WithMany()
                .HasForeignKey("JobId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Skill>()
                .WithMany()
                .HasForeignKey("SkillId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Qualification entity
        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey("QualificationId");
            entity.Property("ProgrammeId").HasColumnName("programme_id").IsRequired();
            entity.Property("JobId").HasColumnName("job_id").IsRequired();

            entity.HasOne<Programme>()
                .WithMany()
                .HasForeignKey("ProgrammeId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<JobListing>()
                .WithMany()
                .HasForeignKey("JobId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Add JobScheme configuration
        modelBuilder.Entity<JobScheme>(entity =>
        {
            entity.HasKey("SchemeId");

            entity.Property("SchemeId")
                .HasColumnName("scheme_id");

            entity.Property("SchemeName")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("scheme_name");

            entity.HasIndex("SchemeName").IsUnique();
        });

        // Add RemunerationType entity
        modelBuilder.Entity<RemunerationType>(entity =>
        {
            entity.HasKey("RemunerationId");

            entity.Property("RemunerationId")
                .HasColumnName("remnmeration_id");

            entity.Property("Type")
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("type");

            entity.HasIndex("Type").IsUnique();
        });


    }
}