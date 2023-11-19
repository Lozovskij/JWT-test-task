using Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;

    public IUserRepository UsersRepository { get; private set; }
    public IUserRequestRepository UserRequestRepository { get; private set; }

    public UnitOfWork(
        DataContext context,
        IUserRepository userRepository,
        IUserRequestRepository userRequestRepository)
    {
        _context = context;
        UsersRepository = userRepository;
        UserRequestRepository = userRequestRepository;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
