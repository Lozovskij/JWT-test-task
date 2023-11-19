using Core.Abstractions;
using Core.Models;

namespace DataAccess.Repositories;

public class UserRequestRepository : IUserRequestRepository
{
    private readonly DataContext _context;
    public UserRequestRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserRequest userRequest, CancellationToken cancellationToken)
    {
        await _context.UserRequests.AddAsync(userRequest, cancellationToken);
    }
}
