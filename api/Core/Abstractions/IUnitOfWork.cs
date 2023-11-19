namespace Core.Abstractions;

public interface IUnitOfWork
{
    IUserRepository UsersRepository { get; }
    IUserRequestRepository UserRequestRepository { get; }

    Task CompleteAsync();
}
