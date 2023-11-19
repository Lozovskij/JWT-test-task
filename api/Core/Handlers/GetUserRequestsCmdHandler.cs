using Core.Abstractions;
using Core.Models;
using MediatR;

namespace Core.Handlers;

public class GetUserRequestsCmdHandler : IRequestHandler<GetUserRequestsCommand, Result<List<UserRequest>, Error>>
{
    private readonly IUnitOfWork _uow;
    private readonly IUserService _userService;

    public GetUserRequestsCmdHandler(IUnitOfWork uow, IUserService userService)
    {
        _uow = uow;
        _userService = userService;
    }

    public async Task<Result<List<UserRequest>, Error>> Handle(GetUserRequestsCommand request, CancellationToken cancellationToken)
    {
        var username = _userService.GetUsername();
        var user = await _uow.UsersRepository.TryGetByNameAsync(username, cancellationToken);
        if (user == null)
        {
            return new Error("Can't find the user by username", HttpStatusCode.NotFound);
        }
        var userRequests = await _uow.UserRequestRepository.GetActiveRequestsByUserIdAsync(user.Id, cancellationToken);
        return userRequests.ToList();
    }
}

public record GetUserRequestsCommand()
    : IRequest<Result<List<UserRequest>, Error>>;