using MediatR;

namespace EventPlanner.Application.Features.User.RegisterUser;

public record RegisterUserCommand : IRequest<Guid>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}