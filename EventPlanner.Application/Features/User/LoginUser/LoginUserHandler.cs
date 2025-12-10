using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Application.Features.User.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, LoginDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherApp _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    public LoginUserHandler(IUserRepository userRepository, IPasswordHasherApp passwordHasher, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }
    public async Task<LoginDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        var verficationResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );
        if (verficationResult != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedException("Invalid credentials");
        }

        var token = _jwtTokenService.GenerateToken(user);
        
        return new LoginDto
        {
            UserId = user.Id,
            Token = token
        };
    }
}

