using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Repositories;
using EventPlanner.Domain.ValueObjects;
using MediatR;

namespace EventPlanner.Application.Features.User.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherApp _passwordHasher;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordHasherApp passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
        {
            throw new UserAlreadyExistsException(request.Email);
        }

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var newUser = new Domain.Entities.User
        (Guid.NewGuid(),
         new EmailAddress(request.Email),
         request.FirstName,
         request.LastName);

        newUser.SetPasswordHash(hashedPassword);

        await _userRepository.AddAsync(newUser, cancellationToken);

        return newUser.Id;
    }
}