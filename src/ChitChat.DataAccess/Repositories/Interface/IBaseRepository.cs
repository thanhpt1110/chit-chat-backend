using System.Linq.Expressions;

using ChitChat.Domain.Common;
using ChitChat.Domain.Entities;

using Microsoft.EntityFrameworkCore.Query;

namespace ChitChat.DataAccess.Repositories.Interrface
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQuery();

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<Expression<Func<TEntity, BaseEntity>>> includes);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<Expression<Func<TEntity, BaseEntity>>> includes);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort);

        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
            IEnumerable<Expression<Func<TEntity, BaseEntity>>> includes);

        Task<PaginationResponse<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort, int pageIndex, int pageSize);

        Task<PaginationResponse<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
            int pageIndex,
            int pageSize,
            IEnumerable<Expression<Func<TEntity, BaseEntity>>> includes);

        Task<PaginationResponse<TEntity>> GetAllAsync(
           Expression<Func<TEntity, bool>> predicate,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
           int pageIndex,
           int pageSize,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery);

        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
