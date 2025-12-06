using MediatR;

namespace EventPlanner.Application.Features.User.RegisterUser;

public record RegisterUserCommand : IRequest<Guid>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}