using System.Linq.Expressions;
using Core.Constant;
using Core.QueryExpression;
using Core.Repository;
using Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Core.EntityFramework.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public async Task BulkDeleteAsync(IUnitOfWork unitOfWork, ICollection<TEntity> entities,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return;
        dbContext.RemoveRange(entities);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(IUnitOfWork unitOfWork, TEntity entity,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return;

        dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext)
        {
            return null;
        }

        var donus = await dbContext.Set<TEntity>().Where<TEntity>(predicate)
            .FirstOrDefaultAsync<TEntity>(cancellationToken: cancellationToken);

        return donus;
    }

    public async Task<TEntity?> GetAsync(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> predicate,
        string include, CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return null;

        return await dbContext.Set<TEntity>().Where<TEntity>(predicate).Include<TEntity>(include)
            .FirstOrDefaultAsync<TEntity>(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<TEntity>?> GetListAsync(IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return null;

        return await Task.Run(() => dbContext.Set<TEntity>().ToList(), cancellationToken);
    }

    public async Task<IEnumerable<TEntity>?> GetListAsync(IUnitOfWork unitOfWork, object predicate,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return null;

        return await Task.Run(() => dbContext.Set<TEntity>().ToList(), cancellationToken);
    }

    public async Task<IEnumerable<TEntity>?> GetListAsync(IUnitOfWork unitOfWork,
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return null;

        return await Task.Run(() => dbContext.Set<TEntity>().Where(predicate).ToList(), cancellationToken);
    }

    public async Task<IEnumerable<TEntity>?> GetListAsync(IUnitOfWork unitOfWork,
        Expression<Func<TEntity, bool>> predicate, string include,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return null;

        return await Task.Run(() => dbContext.Set<TEntity>().Where(predicate).Include<TEntity>(include).ToList(),
            cancellationToken);
    }

    public async Task<PagingResponse<TEntity>> GetPaginationAsync(IUnitOfWork unitOfWork, PagingRequest request,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return null;

        return await Task.Run(() =>
        {
            var response = new PagingResponse<TEntity>();

            Expression<Func<TEntity, bool>>? predicate = null;

            if ((request.Parameters.Count > 0))
            {
                predicate = request.Parameters.ToFilterExpression<TEntity>();
            }

            request.OrderByType ??= OrderByType.Asc;
            request.Skip ??= 0;

            var source = (predicate != null)
                ? dbContext.Set<TEntity>().Where<TEntity>(predicate).AsQueryable<TEntity>()
                : dbContext.Set<TEntity>().AsQueryable<TEntity>();

            response.TotalCount = source.Count<TEntity>();
            request.Take ??= response.TotalCount;
            response.Result = source.ApplyOrdering<TEntity>(request.OrderColumn, request.OrderByType.Value)
                .Skip<TEntity>(request.Skip.Value).Take<TEntity>(request.Take.Value);

            return response;
        }, cancellationToken);
    }

    public async Task<PagingResponse<TQueryModel>> GetPaginationAsync<TQueryModel>(IQueryable<TQueryModel> query,
        PagingRequest request, CancellationToken cancellationToken)
        where TQueryModel : class
    {
        return await Task.Run(() =>
        {
            var response = new PagingResponse<TQueryModel>();

            Expression<Func<TQueryModel, bool>>? predicate = null;

            if ((request.Parameters.Count > 0))
            {
                predicate = request.Parameters.ToFilterExpression<TQueryModel>();
            }

            request.OrderByType ??= OrderByType.Asc;
            request.Skip ??= 0;

            var source = (predicate != null)
                ? query.Where<TQueryModel>(predicate).AsQueryable<TQueryModel>()
                : query.AsQueryable<TQueryModel>();

            response.TotalCount = source.Count<TQueryModel>();
            request.Take ??= response.TotalCount;
            response.Result = source.ApplyOrdering<TQueryModel>(request.OrderColumn, request.OrderByType.Value)
                .Skip<TQueryModel>(request.Skip.Value).Take<TQueryModel>(request.Take.Value)
                .AsEnumerable<TQueryModel>();

            return response;
        }, cancellationToken);
    }

    public async Task InsertAsync(IUnitOfWork unitOfWork, TEntity entity,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return;

        dbContext.Set<TEntity>().Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(IUnitOfWork unitOfWork, TEntity entity,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return;

        if (dbContext.Entry<TEntity>(entity).State == EntityState.Detached)
        {
            dbContext.Attach(entity);
        }

        dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BulkInsertAsync(IUnitOfWork unitOfWork, ICollection<TEntity> entities,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return;

        dbContext.Set<TEntity>().AddRange(entities);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BulkUpdateAsync(IUnitOfWork unitOfWork, ICollection<TEntity> entities,
        CancellationToken cancellationToken)
    {
        if (unitOfWork.Context is not DbContext dbContext) return;

        dbContext.Set<TEntity>().UpdateRange(entities);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}