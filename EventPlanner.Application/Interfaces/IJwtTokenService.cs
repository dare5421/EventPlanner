namespace EventPlanner.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Domain.Entities.User user);
}
