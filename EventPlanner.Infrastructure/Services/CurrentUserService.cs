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
        var claimPrincipal = _httpContextAccessor.HttpContext?.User;
        if (claimPrincipal == null || !claimPrincipal.Identity?.IsAuthenticated == true)
        {
            return null;
        }
        var userIdClaim = claimPrincipal.FindFirst("sub") 
            ?? claimPrincipal.FindFirst("uid") 
            ?? claimPrincipal.FindFirst("UserId");
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return null;
        }
        return userId;
    }
}
