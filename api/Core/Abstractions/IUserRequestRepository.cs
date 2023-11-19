using Core.Models;

namespace Core.Abstractions;

public interface IUserRequestRepository
{
    public Task AddAsync(UserRequest user, CancellationToken cancellationToken);
    Task<IEnumerable<UserRequest>> GetActiveRequestsByUserIdAsync(int id, CancellationToken cancellationToken);
    Task<UserRequest?> TryFindByIdAsync(int requestId, CancellationToken cancellationToken);
}
