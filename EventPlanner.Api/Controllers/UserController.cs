using EventPlanner.Application.Features.User.LoginUser;
using EventPlanner.Application.Features.User.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Registers a new user account.
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)] // For UserAlreadyExistsException
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        Guid userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id = userId }, userId);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
    {
        LoginDto loginResult = await _mediator.Send(query);
        return Ok(loginResult);
    }
}