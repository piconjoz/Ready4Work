namespace backend.Data;

using Microsoft.EntityFrameworkCore;
using backend.Components.Company.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    // this represents the companies table
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // conf company table
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
    }
}