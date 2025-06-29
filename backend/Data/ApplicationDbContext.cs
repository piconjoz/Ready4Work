namespace backend.Data;

using Microsoft.EntityFrameworkCore;
using backend.Components.Company.Models;
using backend.User.Models;
using backend.Components.User.Models;
using backend.Components.JobListing.Models;

public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<RemunerationType> RemunerationTypes { get; set; }
        public DbSet<JobScheme> JobSchemes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }



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

            entity.Property("RenumerationType")
                .HasColumnName("renumeration_type");

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
        
        // Remuneration Type
        modelBuilder.Entity<RemunerationType>(entity =>
        {
            entity.HasKey("RemunerationId");
            
            entity.Property("RemunerationId")
                .HasColumnName("renumeration_id");
                
            entity.Property("Type")
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("type");
                
            entity.HasIndex("Type").IsUnique();
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

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey("SkillId");
            entity.Property("skill").HasColumnName("skill");
        });

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

    }
    }