using Core.EntityFramework.Repository;
using Core.Repository;
using Core.UnitOfWork;
using WebApi.Database.DbContext;
using WebApi.Database.Entity;

namespace WebApi.Database.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public async Task<PagingResponse<User>> GetUsersAsync(IUnitOfWork unitOfWork, PagingRequest pagingRequest,
        CancellationToken cancellationToken = default)
    {
        var context = unitOfWork.Context as AppDbContext;

        var query = (from u in context.Users select u);

        return await GetPaginationAsync(query, pagingRequest, cancellationToken);
    }
}