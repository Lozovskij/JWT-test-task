using Core.Abstractions;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly DataContext _context;
    public RequestRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Request>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Requests.ToListAsync(cancellationToken);
    }
}
