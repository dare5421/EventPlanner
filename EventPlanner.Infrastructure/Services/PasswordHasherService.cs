// EventPlanner.Infrastructure/Services/PasswordHasherService.cs
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Infrastructure.Services;

public class PasswordHasherService : IPasswordHasherApp // <-- Make sure this implements your interface
{
    private readonly IPasswordHasher<User> _frameworkHasher;

    public PasswordHasherService(IPasswordHasher<User> frameworkHasher)
    {
        _frameworkHasher = frameworkHasher;
    }

    // FIX 1: Implement the simplified hashing method
    public string HashPassword(string password)
    {
        // We pass 'null' to the TUser argument of the framework's hasher to satisfy its contract
        // while using the simple signature required by the Application Layer.
        return _frameworkHasher.HashPassword(null, password);
    }

    // FIX 2: Verification method remains as is
    public PasswordVerificationResult VerifyHashedPassword(
        User user,
        string hashedPassword,
        string providedPassword)
    {
        return _frameworkHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
    }
}