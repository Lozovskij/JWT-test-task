using Core.Models;

namespace Core.Abstractions;

public interface IUserRequestRepository
{
    public Task AddAsync(UserRequest user, CancellationToken cancellationToken);
}
