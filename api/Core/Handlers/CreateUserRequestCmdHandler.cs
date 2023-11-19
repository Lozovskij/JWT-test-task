using Core.Abstractions;
using Core.Models;
using MediatR;

namespace Core.Handlers;

public class CreateUserRequestCmdHandler : IRequestHandler<CreateUserRequestCommand, Result<UserRequest, Error>>
{
    private readonly IUnitOfWork _uow;

    public CreateUserRequestCmdHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result<UserRequest, Error>> Handle(CreateUserRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UsersRepository.TryGetByNameAsync(request.Username, cancellationToken);
        if (user == null) throw new InvalidOperationException();
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