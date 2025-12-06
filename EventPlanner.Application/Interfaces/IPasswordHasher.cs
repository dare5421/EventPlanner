using EventPlanner.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Application.Interfaces;
public interface IPasswordHasher : IPasswordHasher<User>
{
    // No implementation needed here. We simply inherit the methods:
    // 1. HashPassword(User user, string password)
    // 2. VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
}