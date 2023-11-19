using Core.Abstractions;

namespace DataAccess;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;

    public IUserRepository UsersRepository { get; private set; }
    public IUserRequestRepository UserRequestRepository { get; private set; }
    public IRequestRepository RequestRepository { get; private set; }

    public UnitOfWork(
        DataContext context,
        IUserRepository userRepository,
        IUserRequestRepository userRequestRepository,
        IRequestRepository requestRepository)
    {
        _context = context;
        UsersRepository = userRepository;
        UserRequestRepository = userRequestRepository;
        RequestRepository = requestRepository;
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
