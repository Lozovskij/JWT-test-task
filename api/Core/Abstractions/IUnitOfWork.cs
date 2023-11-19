namespace Core.Abstractions;

public interface IUnitOfWork
{
    IUserRepository UsersRepository { get; }

    Task CompleteAsync();
}
