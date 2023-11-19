using Core.Abstractions;
using Core.Handlers;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<UserRequestDto>> Create(UserRequestDto request, CancellationToken cancellationToken)
    {
        var command = new CreateUserRequestCommand(request.Username, request.RequestId, request.UserComment);
        var result = await _mediator.Send(command, cancellationToken);
        return result.Match<ActionResult<UserRequestDto>>(
            ur => Ok(new UserRequestDto(ur)), e => StatusCode((int)e.StatusCode, e.Message));
    }
}
