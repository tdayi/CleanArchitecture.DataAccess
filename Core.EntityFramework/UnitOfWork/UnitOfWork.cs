using Core.UnitOfWork;

namespace Core.EntityFramework.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public object Context { get; }

    private readonly IDbContextFactory dbContextFactory;

    public UnitOfWork(IDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
        Context = dbContextFactory.GetDbContext();
    }

    public async Task BeginAsync(CancellationToken cancellationToken)
    {
        await dbContextFactory.BeginAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await dbContextFactory.CommitAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await dbContextFactory.DisposeAsync();
    }
}