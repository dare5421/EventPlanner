using EventPlanner.Application.Features.User.RegisterUser;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using EventPlanner.Infrastructure.Data;
using EventPlanner.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using EventPlanner.Infrastructure.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

// 1. Register MediatR, scanning the Application assembly for commands/handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));

// 2. Register the framework's default PasswordHasher implementation
// You must use the FULL NAMESPACE for the framework's IPasswordHasher<T>
builder.Services.AddScoped<
    Microsoft.AspNetCore.Identity.IPasswordHasher<EventPlanner.Domain.Entities.User>,
    Microsoft.AspNetCore.Identity.PasswordHasher<EventPlanner.Domain.Entities.User>>();

// 3. Register your custom adapter service (IPasswordHasher is YOUR interface)
builder.Services.AddScoped<IPasswordHasherApp, PasswordHasherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
