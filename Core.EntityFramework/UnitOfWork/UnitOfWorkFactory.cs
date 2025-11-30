using Core.UnitOfWork;

namespace Core.EntityFramework.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public IUnitOfWork Create()
    {
        return new UnitOfWork(_dbContextFactory);
    }
}