using Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Database.DbContext;

public class AppDbContextFactory : IDbContextFactory
{
    private readonly AppDbContext _appDbContext;

    public AppDbContextFactory(DbContextOptions<AppDbContext> _dbContextOptions)
    {
        _appDbContext = new AppDbContext(_dbContextOptions);
    }

    public object GetDbContext()
    {
        return _appDbContext;
    }

    public async Task BeginAsync(CancellationToken cancellationToken)
    {
        await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _appDbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task DisposeAsync()
    {
        await _appDbContext.DisposeAsync();
    }
}