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
// using backend.User.Repositories.Interfaces;
// using backend.User.Repositories;
// using backend.User.Services.Interfaces;
// using backend.User.Services;
using backend.Components.User.Repositories.Interfaces;
using backend.Components.User.Repositories;
using backend.Components.User.Services;
using backend.Components.User.Services.Interfaces;

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

// BACK TO IN-MEMORY DATABASE FOR DEVELOPMENT
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseInMemoryDatabase("Ready4WorkTestDB"));

// under appsettings.json, please change the relevant Default connections
//   "ConnectionStrings": {
//     "DefaultConnection": "server=localhost(usually, unless using outside connection);port=3306;database=YourDatabase;user=yourMySqlUser;password=yourMySqlPassword;"
//   }
// if possible, use a development appsetting to ensure your fields are not exposed
// i.e. appsettings.Development.json
// before running your dotnet migrations and build, run the following:
// SET ASPNETCORE_ENVIRONMENT=Development
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));


// COMMENT OUT MYSQL (MIGRATIONS ARE GENERATED, DON'T NEED IT FOR NOW)
/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL("Server=localhost;Port=3306;Database=Ready4WorkTemp;Uid=root;Pwd=temp;"));
*/

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

// BACK TO IN-MEMORY DATABASE CREATION
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

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