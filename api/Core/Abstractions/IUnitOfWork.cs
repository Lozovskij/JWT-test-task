namespace Core.Abstractions;

public interface IUnitOfWork
{
    IUserRepository UsersRepository { get; }
    IUserRequestRepository UserRequestRepository { get; }
    IRequestRepository RequestRepository { get; }

    Task CompleteAsync();
}
