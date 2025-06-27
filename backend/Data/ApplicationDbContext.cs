namespace backend.Data;

using Microsoft.EntityFrameworkCore;
using backend.Components.Company.Models;
using backend.User.Models;

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
        });

        // user entity - ef core can handle private properties automatically
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey("UserId");
            
            // just configure the properties normally - no need for hasfield
            entity.Property("NRIC")
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("nric");
                
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
            entity.HasIndex("NRIC").IsUnique();
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
    }
}