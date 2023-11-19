using Core.Abstractions;
using Core.Models;
using MediatR;

namespace Core.Handlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string, Error>>
{
    private readonly IUnitOfWork _uow;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUnitOfWork uow, ITokenService tokenService)
    {
        _uow = uow;
        _tokenService = tokenService;
    }

    public async Task<Result<string, Error>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UsersRepository.TryGetByNameAsync(request.Username, cancellationToken);
        if (user == null)
        {
            return new Error("User not found", HttpStatusCode.NotFound);
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return new Error("Wrong password", HttpStatusCode.BadRequest);
        }

        string accessToken = _tokenService.CreateAccessToken(user.Username);

        await _tokenService.SetRefreshTokenAsync(user.Username);

        return accessToken;
    }
}

public record LoginUserCommand(string Username, string Password) : IRequest<Result<string, Error>>;