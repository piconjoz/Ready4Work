// backend/Program.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend.Data;
using backend.Components.Company.Repository;
using backend.Components.Company.Services;
using backend.Components.AI.Services;               
using backend.Components.Application.Services;           
using backend.Components.Application.Repository;
using backend.Components.CoverLetter.Services;           
using backend.Components.CoverLetter.Repository;        
using backend.User.Services.Interfaces;                  
using backend.User.Services;                            
using backend.User.Repositories.Interfaces;             
using backend.User.Repositories;                         
using backend.Components.Resume.Repository;
using backend.Components.Resume.Services;
using backend.Components.Bookmark.Repository;
using backend.Components.Bookmark.Services;
using backend.Components.Company.Services.Interfaces;
using backend.Components.User.Repositories.Interfaces;
using backend.Components.User.Repositories;
using backend.Components.User.Services;
using backend.Components.User.Services.Interfaces;
using backend.Components.JobListing.Repositories.Interfaces;
using backend.Components.JobListing.Repositories;
using backend.Components.JobListing.Services.Interfaces;
using backend.Components.JobListing.Services;
using backend.Components.Student.Repositories.Interfaces;
using backend.Components.Student.Repositories;
using backend.Components.Student.Services.Interfaces;
using backend.Components.Student.Services;
using MySql.Data.MySqlClient; // Add this using statement
using System.Threading;      // Add this using statement
using System;                // Add this using statement


DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// add services to the container.
builder.Services.AddControllers();

// configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// register dependency injection for company repositories and services
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

// HTTP Client for AI services
builder.Services.AddHttpClient<IAIService, AIService>();

// AI and Application services
builder.Services.AddScoped<IAIService, AIService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

// CoverLetter services (NEW)
builder.Services.AddScoped<ICoverLetterService, CoverLetterService>();
builder.Services.AddScoped<ICoverLetterRepository, CoverLetterRepository>();

// User services (if not already registered)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

// Register Resume components
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<IResumeService, ResumeService>();

// Register Bookmark components
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();

// register dependency injection for user repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<IRecruiterRepository, RecruiterRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

// register dependency injection for user services
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<IRecruiterService, RecruiterService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// register dependency injection for job listing components
builder.Services.AddScoped<IJobListingRepository, JobListingRepository>();
builder.Services.AddScoped<IJobListingService, JobListingService>();

// StudentProfile components
builder.Services.AddScoped<IStudentProfileRepository, StudentProfileRepository>();
builder.Services.AddScoped<IStudentProfileService, StudentProfileService>();

// configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("JWT SecretKey is not configured in appsettings.json");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// *** IMPORTANT: ADD RETRY LOGIC HERE ***
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var retries = 50; 
    var delay = 1000; 

    for (int i = 0; i < retries; i++)
    {
        try
        {
            Console.WriteLine($"Attempting to connect to database... (Attempt {i + 1}/{retries})");
            context.Database.EnsureCreated(); 
            Console.WriteLine("Database connection successful and ensured created.");
            break; 
        }
        catch (MySqlException ex)
        {
            // Check if the inner exception is a SocketException with 'Connection refused' error code (10061 for Windows, 111 for Linux)
            if (ex.InnerException is System.Net.Sockets.SocketException socketEx && 
                (socketEx.ErrorCode == 10061 || socketEx.ErrorCode == 111))
            {
                Console.WriteLine($"Database connection failed (Connection refused from MySqlException): {ex.Message}. Retrying in {delay / 1000} seconds...");
                Thread.Sleep(delay);
            }
            else
            {
                // If it's a MySqlException but not a connection refused, re-throw
                Console.WriteLine($"An unretryable MySqlException occurred: {ex.Message}");
                throw; 
            }
        }
        catch (System.Net.Sockets.SocketException ex) // Directly catch SocketException too
        {
            // Explicitly catch SocketException if it occurs directly (though often wrapped by MySqlException)
            if (ex.ErrorCode == 10061 || ex.ErrorCode == 111)
            {
                Console.WriteLine($"Database connection failed (SocketException: Connection refused): {ex.Message}. Retrying in {delay / 1000} seconds...");
                Thread.Sleep(delay);
            }
            else
            {
                Console.WriteLine($"An unretryable SocketException occurred: {ex.Message}");
                throw;
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"An unexpected general error occurred during database connection: {ex.Message}");
            throw; 
        }

        if (i == retries - 1)
        {
            Console.WriteLine("Exceeded maximum retries. Could not connect to the database.");
            throw new Exception("Failed to connect to the database after multiple retries.");
        }
    }
}
// *** END RETRY LOGIC ***

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();

// authentication and authorization must be in this order
app.UseAuthentication(); // add this line - validates JWT tokens
app.UseAuthorization();  // this was already here

app.MapControllers();

app.Run();