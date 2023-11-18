using Core.Models;

namespace Core.Abstractions;

public interface IUserRepository
{
    public Task AddUserAsync(User user);
}
