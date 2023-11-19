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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserRequestDto>>> GetAll(CancellationToken cancellationToken)
    {
        var command = new GetUserRequestsCommand();
        var result = await _mediator.Send(command, cancellationToken);
        return result.Match(userRequests =>
            Ok(userRequests.Select(ur => new UserRequestDto(ur))),
            e => StatusCode((int)e.StatusCode, e.Message));
    }

    [HttpPost("create")]
    public async Task<ActionResult<UserRequestDto>> Create(UserRequestDto request, CancellationToken cancellationToken)
    {
        var command = new CreateUserRequestCommand(request.Username, request.RequestId, request.UserComment);
        var result = await _mediator.Send(command, cancellationToken);
        return result.Match(ur => Ok(new UserRequestDto(ur)), e => StatusCode((int)e.StatusCode, e.Message));
    }

    [HttpPost("cancel")]
    public async Task<ActionResult> Cancel(CancelRequestDto request, CancellationToken cancellationToken)
    {
        var command = new CancelUserRequestCommand(request.UserRequestId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.Match<ActionResult>(_ => NoContent(), e => StatusCode((int)e.StatusCode, e.Message));
    }
}
