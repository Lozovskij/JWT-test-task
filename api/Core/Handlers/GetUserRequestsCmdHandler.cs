using Core.Abstractions;
using Core.Constants;
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
        var user = await _userService.TryGetUser(cancellationToken);
        if (user == null)
        {
            return Errors.NoCurrentUser;
        }
        var userRequests = await _uow.UserRequestRepository.GetActiveRequestsByUserIdAsync(user.Id, cancellationToken);
        return userRequests.OrderByDescending(ur => ur.CreatedDate).ToList();
    }
}

public record GetUserRequestsCommand()
    : IRequest<Result<List<UserRequest>, Error>>;