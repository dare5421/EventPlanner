using MediatR;

namespace EventPlanner.Application.Features.User.LoginUser;

public record LoginUserQuery : IRequest<LoginDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
