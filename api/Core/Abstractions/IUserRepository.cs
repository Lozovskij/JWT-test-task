using Core.Models;
using System.Threading;

namespace Core.Abstractions;

public interface IUserRepository
{
    public Task AddAsync(User user, CancellationToken cancellationToken);
    void SetRefreshToken(string username, string token, DateTime expires);
    Task<User?> TryGetByNameAsync(string username, CancellationToken cancellationToken);
    Task<bool> UsernameExistAsync(string username, CancellationToken cancellationToken);
}
