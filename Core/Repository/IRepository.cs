using System.Linq.Expressions;
using Core.UnitOfWork;

namespace Core.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    Task BulkDeleteAsync(IUnitOfWork unitOfWork, ICollection<TEntity> entities,
        CancellationToken cancellationToken);

    Task DeleteAsync(IUnitOfWork unitOfWork, TEntity entity, CancellationToken cancellationToken);

    Task<TEntity?> GetAsync(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<TEntity?> GetAsync(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> predicate, string include,
        CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>?> GetListAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>?> GetListAsync(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>?> GetListAsync(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> predicate,
        string include, CancellationToken cancellationToken);

    Task<PagingResponse<TEntity>> GetPaginationAsync(IUnitOfWork unitOfWork, PagingRequest request,
        CancellationToken cancellationToken);

    Task<PagingResponse<TQueryModel>> GetPaginationAsync<TQueryModel>(IQueryable<TQueryModel> query,
        PagingRequest request, CancellationToken cancellationToken)
        where TQueryModel : class;

    Task InsertAsync(IUnitOfWork unitOfWork, TEntity entity, CancellationToken cancellationToken);

    Task UpdateAsync(IUnitOfWork unitOfWork, TEntity entity, CancellationToken cancellationToken);

    Task BulkInsertAsync(IUnitOfWork unitOfWork, ICollection<TEntity> entities,
        CancellationToken cancellationToken);
        
    Task BulkUpdateAsync(IUnitOfWork unitOfWork, ICollection<TEntity> entities,
        CancellationToken cancellationToken);
}