namespace EventPlanner.Application.Features.User.LoginUser;

public class LoginDto
{
    public required Guid UserId { get; init; }
    public required string Token { get; init; }
}