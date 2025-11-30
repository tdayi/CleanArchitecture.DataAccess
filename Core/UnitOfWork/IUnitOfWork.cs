namespace Core.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    object Context { get; }
    Task BeginAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
}