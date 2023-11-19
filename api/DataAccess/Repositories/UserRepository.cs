using Core.Abstractions;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public void SetRefreshToken(string username, string token, DateTime expires)
    {
        var user = _context.Users.First(u => u.Username == username);
        user.RefreshToken = token;
        user.TokenExpires = expires;
    }

    public async Task<User?> TryGetByNameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<bool> UsernameExistAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(u =>  u.Username == username, cancellationToken);
    }
}
