using Core.Abstractions;
using Core.Models;
using MediatR;

namespace Core.Handlers;

public class CancelUserRequestCmdHandler : IRequestHandler<CancelUserRequestCommand, Result<bool, Error>>
{
    private readonly IUnitOfWork _uow;
    private readonly IUserService _userService;

    public CancelUserRequestCmdHandler(IUnitOfWork uow, IUserService userService)
    {
        _uow = uow;
        _userService = userService;
    }

    public async Task<Result<bool, Error>> Handle(CancelUserRequestCommand request, CancellationToken cancellationToken)
    {
        var userRequest = await _uow.UserRequestRepository.TryFindByIdAsync(request.UserRequestId, cancellationToken);
        if (userRequest == null)
        {
            return new Error("Can't find user request", HttpStatusCode.NotFound);
        }

        //TODO move to separate auth manager service
        var username = _userService.GetUsername();
        var user = await _uow.UsersRepository.TryGetByNameAsync(username, cancellationToken);
        if (user == null)
        {
            return new Error("Can't define current user", HttpStatusCode.Unauthorized);
        }
        if (user.Id != userRequest.UserId)
        {
            return new Error("No access, request doesn't belong to the user", HttpStatusCode.Unauthorized);
        }

        userRequest.Status = RequestStatus.Canceled;
        await _uow.CompleteAsync();
        return true;
    }
}

public record CancelUserRequestCommand(int UserRequestId) : IRequest<Result<bool, Error>>;