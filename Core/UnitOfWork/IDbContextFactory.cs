namespace Core.UnitOfWork;

public interface IDbContextFactory
{
    object GetDbContext();
    Task BeginAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task DisposeAsync();
}