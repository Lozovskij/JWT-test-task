using Core.Abstractions;
using Core.Models;
using MediatR;

namespace Core.Handlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<User, Error>>
{
    private readonly IUnitOfWork _uow;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Result<User, Error>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _uow.UsersRepository.UsernameExistAsync(request.Username, cancellationToken))
        {
            return new Error("Username exist", HttpStatusCode.Conflict);
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User() { PasswordHash = passwordHash, Username = request.Username };
        await _uow.UsersRepository.AddAsync(user, cancellationToken);

        await _uow.CompleteAsync();
        return user;
    }
}

public record RegisterUserCommand(string Username, string Password) : IRequest<Result<User, Error>>;
