using Core;
using Core.Abstractions;
using Core.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<UserRequest>> GetActiveRequestsByUserIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.UserRequests.Include(ur => ur.Request)
            .Where(ur => ur.Status == RequestStatus.Active && ur.UserId == id)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserRequest?> TryFindByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.UserRequests.FindAsync(id, cancellationToken);
    }
}
