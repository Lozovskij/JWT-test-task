using Core.Models;
using System.Threading;

namespace Core.Abstractions;

public interface IUserRepository
{
    public Task AddUserAsync(User user, CancellationToken cancellationToken);
    void SetRefreshToken(string username, string token, DateTime expires);
    Task<User?> TryGetUserByNameAsync(string username, CancellationToken cancellationToken);
    Task<bool> UsernameExistAsync(string username, CancellationToken cancellationToken);
}
