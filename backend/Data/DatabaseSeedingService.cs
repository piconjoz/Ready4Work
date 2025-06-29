using backend.User.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class DatabaseSeedingService
{
    private readonly IAdminService _adminService;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DatabaseSeedingService> _logger;

    public DatabaseSeedingService(
        IAdminService adminService,
        ApplicationDbContext context,
        ILogger<DatabaseSeedingService> logger)
    {
        _adminService = adminService;
        _context = context;
        _logger = logger;
    }

    public async Task SeedAdminAccountsAsync()
    {
        _logger.LogInformation("Starting admin account seeding...");

        try
        {
            // Check if any admins already exist
            var existingAdmins = await _context.Admins.CountAsync();
            if (existingAdmins > 0)
            {
                _logger.LogInformation($"Admin accounts already exist ({existingAdmins} found). Skipping seeding.");
                return;
            }

            // Create default admin accounts
            var adminAccounts = new[]
            {
                new
                {
                    Email = "admin@sit.singaporetech.edu.sg",
                    FirstName = "System",
                    LastName = "Administrator",
                    Phone = "65551234",
                    Gender = "Male",
                    Password = "Admin123!@#" // In production, use environment variable
                },
                new
                {
                    Email = "super.admin@sit.singaporetech.edu.sg",
                    FirstName = "Super",
                    LastName = "Admin",
                    Phone = "65551235",
                    Gender = "Female",
                    Password = "SuperAdmin123!@#" // In production, use environment variable
                },
                new
                {
                    Email = "registrar@sit.singaporetech.edu.sg",
                    FirstName = "Registrar",
                    LastName = "Office",
                    Phone = "65551236",
                    Gender = "Male",
                    Password = "Registrar123!@#" // In production, use environment variable
                }
            };

            foreach (var adminData in adminAccounts)
            {
                try
                {
                    _logger.LogInformation($"Creating admin account for {adminData.Email}...");

                    var adminAccount = await _adminService.CreateCompleteAdminAccountAsync(
                        adminData.Email,
                        adminData.FirstName,
                        adminData.LastName,
                        adminData.Phone,
                        adminData.Gender,
                        adminData.Password
                    );

                    _logger.LogInformation($"✅ Successfully created admin account: {adminAccount.User?.Email} (Admin ID: {adminAccount.AdminId})");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"❌ Failed to create admin account for {adminData.Email}");
                }
            }

            _logger.LogInformation("Admin account seeding completed!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to seed admin accounts");
            throw;
        }
    }

    public async Task<List<AdminSummary>> GetAdminSummaryAsync()
    {
        var admins = await _adminService.GetAllAdminsAsync();
        var summaries = new List<AdminSummary>();

        foreach (var admin in admins)
        {
            var adminProfile = await _adminService.ConvertToResponseDTOAsync(admin);
            summaries.Add(new AdminSummary
            {
                AdminId = adminProfile.AdminId,
                Email = adminProfile.User?.Email ?? "Unknown",
                FullName = $"{adminProfile.User?.FirstName} {adminProfile.User?.LastName}",
                CreatedAt = adminProfile.CreatedAt,
                IsActive = adminProfile.User?.IsActive ?? false
            });
        }

        return summaries;
    }
}

public class AdminSummary
{
    public int AdminId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}

// Extension method to easily call seeding
public static class DatabaseSeedingExtensions
{
    public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var seedingService = scope.ServiceProvider.GetRequiredService<DatabaseSeedingService>();
        await seedingService.SeedAdminAccountsAsync();
    }
}