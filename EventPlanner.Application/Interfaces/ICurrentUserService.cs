namespace EventPlanner.Application.Interfaces;

public interface ICurrentUserService
{
    Guid? GetCurrentUserId();
}