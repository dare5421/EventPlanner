using System.Security.Claims;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EventPlanner.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    // reading user Claim through IHttpContextAccessor would be implemented here
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid? GetCurrentUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        // 1. Is the user authenticated by the middleware?
        if (user?.Identity?.IsAuthenticated != true)
        {
            return null; // This is the case if Auth middleware failed
        }

        // 2. Find the ID claim using standard and raw names
        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier) // Checks for the long URI
                      ?? user.FindFirst("nameid");              // Checks for the raw JWT name

        // 3. Check if the claim was found AND can be parsed
        if (idClaim == null || !Guid.TryParse(idClaim.Value, out var userId))
        {
            return null;
        }

        return userId;
    }

}
