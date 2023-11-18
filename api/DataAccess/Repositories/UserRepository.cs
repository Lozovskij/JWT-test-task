using Core.Abstractions;
using Core.Models;

namespace DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    public Task AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
