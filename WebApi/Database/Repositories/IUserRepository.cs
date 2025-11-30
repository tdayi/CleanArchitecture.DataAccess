using Core.Repository;
using Core.UnitOfWork;
using WebApi.Database.Entity;

namespace WebApi.Database.Repositories;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<PagingResponse<UserEntity>> GetUsersAsync(IUnitOfWork unitOfWork, PagingRequest pagingRequest,
        CancellationToken cancellationToken = default);
}