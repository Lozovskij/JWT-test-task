using Core.Abstractions;
using Core.Constants;
using Core.Models;
using MediatR;

namespace Core.Handlers;

public class CreateUserRequestCmdHandler : IRequestHandler<CreateUserRequestCommand, Result<UserRequest, Error>>
{
    private readonly IUnitOfWork _uow;
    private readonly IUserService _userService;

    public CreateUserRequestCmdHandler(IUnitOfWork uow, IUserService userService)
    {
        _uow = uow;
        _userService = userService;
    }

    public async Task<Result<UserRequest, Error>> Handle(CreateUserRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.TryGetUser(cancellationToken);
        if (user == null) return Errors.NoCurrentUser;
        var userRequest = new UserRequest()
        {
            UserId = user.Id,
            RequestId = request.RequestId,
            Status = RequestStatus.Active,
            UserComment = request.UserComment,
            CreatedDate = DateTime.UtcNow,
        };
        await _uow.UserRequestRepository.AddAsync(userRequest, cancellationToken);
        await _uow.CompleteAsync();
        return userRequest;
    }
}

public record CreateUserRequestCommand(string Username, int RequestId, string? UserComment)
    : IRequest<Result<UserRequest, Error>>;