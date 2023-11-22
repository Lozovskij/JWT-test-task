using Core.Abstractions;
using Core.Models;
using System.Security.Claims;
using System.Threading;

namespace Web.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _uow;

    public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork uow)
    {
        _httpContextAccessor = httpContextAccessor;
        _uow = uow;
    }

    public string GetUsername()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext is not null)
        {
            result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
        if (result == null)
        {
            throw new InvalidOperationException("Can't find the claim with type Name");
        }

        return result;
    }

    public async Task<User?> TryGetUser(CancellationToken cancellationToken)
    {
        var username = this.GetUsername();
        return await _uow.UsersRepository.TryGetByNameAsync(username, cancellationToken);
    }
}