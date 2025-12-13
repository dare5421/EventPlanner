using EventPlanner.Application.Features.User.RegisterUser;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using EventPlanner.Infrastructure.Data;
using EventPlanner.Infrastructure.Repositories;
using EventPlanner.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using EventPlanner.Api.Middleware; // Assume this is where your custom middleware is

// FIX: Must be here to clear claim mapping BEFORE builder is created
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVICE REGISTRATION ---
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

// Repository Registration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

// MediatR Registration
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));

// Password Hashing Registration
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IPasswordHasherApp, PasswordHasherService>();

// Authentication Services
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// JWT Bearer Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true, // Security Re-enabled
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SecretKey")!)
        )
    };
});

var app = builder.Build();

// --- 2. PIPELINE CONFIGURATION ---

if (app.Environment.IsDevelopment())
{
    // Development database scope (optional for quick checks)
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
    // Use developer page during development
    app.UseDeveloperExceptionPage();
}
else
{
    // Production/Staging setup
    // You would typically re-enable your custom ExceptionHandlingMiddleware here
    // app.UseMiddleware<ExceptionHandlingMiddleware>(); 

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Middleware Order: Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map Controller Endpoints
app.MapControllers();

app.Run();