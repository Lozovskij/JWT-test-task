using Core.Abstractions;
using Core.Handlers;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMediator _mediator;

    public AuthController(IUserService userService, IMediator mediator)
    {
        _userService = userService;
        _mediator = mediator;
    }

    [HttpGet, Authorize]
    public ActionResult<string> GetMyName()
    {
        return Ok(_userService.GetUsername());
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.Username, request.Password);
        var result = await _mediator.Send(command, cancellationToken);
        return result.Match<ActionResult<User>>(u => Ok(u), e => StatusCode((int)e.StatusCode, e.Message));
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Username, request.Password);
        var result = await _mediator.Send(command, cancellationToken);
        return result.Match(accessToken => Ok(accessToken), e => StatusCode((int)e.StatusCode, e.Message));
    }

    [HttpGet("new-access-token")]
    public async Task<ActionResult<string>> RenewAccessToken(string username, CancellationToken cancellationToken)
    {
        var command = new GenerateAccessTokenCommand(username);
        var result = await _mediator.Send(command, cancellationToken);
        return result.Match(accessToken => Ok(accessToken), e => StatusCode((int)e.StatusCode, e.Message));
    }
}
