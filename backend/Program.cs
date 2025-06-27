using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Components.Company.Repository;
using backend.Components.Company.Services;
using backend.Components.AI.Services;               
using backend.Components.Application.Services;           
using backend.Components.Application.Repository;        
using backend.User.Services.Interfaces;                  
using backend.User.Services;                            
using backend.User.Repositories.Interfaces;             
using backend.User.Repositories;                         
using backend.Components.Resume.Repository;
using backend.Components.Resume.Services;

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
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// USE IN-MEMORY DATABASE (WORKING VERSION)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("Ready4WorkTestDB"));

// COMMENT OUT MYSQL (CAUSES THE ERROR)
/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
*/

// register dependency injection for repositories and services
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

// HTTP Client for AI services
builder.Services.AddHttpClient<IAIService, AIService>();

// AI and Application services
builder.Services.AddScoped<IAIService, AIService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

// User services (if not already registered)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();


// Register Resume components
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<IResumeService, ResumeService>();

var app = builder.Build();

// Ensure in-memory database is created
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
app.UseAuthorization();
app.MapControllers();

app.Run();