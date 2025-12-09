// EventPlanner.Application/Interfaces/IPasswordHasherApp.cs
using EventPlanner.Domain.Entities;
using Microsoft.AspNetCore.Identity;
namespace EventPlanner.Application.Interfaces;

public interface IPasswordHasherApp
{
    // 1. Hashing: Only needs the password string. This simplifies the Registration call.
    string HashPassword(string password);

    // 2. Verification: Still needs the User entity for checking the stored hash.
    PasswordVerificationResult VerifyHashedPassword(
        User user,
        string hashedPassword,
        string providedPassword
    );
}