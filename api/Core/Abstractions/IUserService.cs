using Core.Models;

namespace Core.Abstractions;

public interface IUserService
{
    string GetUsername();
    Task<User?> TryGetUser(CancellationToken cancellationToken);
}