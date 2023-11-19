using Core.Models;

namespace Core.Abstractions;

public interface IRequestRepository
{
    public Task<List<Request>> GetAllAsync(CancellationToken cancellationToken);
}
