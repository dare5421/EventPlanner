using EventPlanner.Application.Features.User.RegisterUser;
using EventPlanner.Application.Interfaces; // <-- FIX: Added for IPasswordHasherApp
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using EventPlanner.Infrastructure.Data;
using EventPlanner.Infrastructure.Repositories;
using EventPlanner.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity; // <-- FIX: Added for IPasswordHasher<T>
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// FIX: Add Controllers for API endpoints
builder.Services.AddControllers();

// DANGER: Removed builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

// 1. Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));

// 2. Register the framework's default PasswordHasher implementation
builder.Services.AddScoped<
    Microsoft.AspNetCore.Identity.IPasswordHasher<EventPlanner.Domain.Entities.User>,
    Microsoft.AspNetCore.Identity.PasswordHasher<EventPlanner.Domain.Entities.User>>();

// 3. Register your custom adapter service (IPasswordHasher is YOUR interface)
builder.Services.AddScoped<IPasswordHasherApp, PasswordHasherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // DANGER: Removed app.MapOpenApi();
}

app.UseHttpsRedirection();

// FIX: Map Controller Endpoints
app.MapControllers();

app.Run();