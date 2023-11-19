using Core.Handlers;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestsController : ControllerBase
{

    private readonly IMediator _mediator;

    public RequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetAll(CancellationToken cancellationToken)
    {
        var command = new GetRequestsCommand();
        var result = await _mediator.Send(command, cancellationToken);
        return result.Match(requests =>
            Ok(requests.Select(request => new RequestDto(request))),
            e => StatusCode((int)e.StatusCode, e.Message));
    }

}
