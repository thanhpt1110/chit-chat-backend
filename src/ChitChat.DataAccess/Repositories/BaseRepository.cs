using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Common;
using ChitChat.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ChitChat.Domain.Exceptions;
using ChitChat.DataAccess.Data;

namespace ChitChat.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQuery() => this.DbSet;

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await DbSet.Where(predicate).FirstOrDefaultAsync();

            return entity ?? throw new ResourceNotFoundException(typeof(TEntity));
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<Expression<Func<TEntity, BaseEntity>>> includes)
        {
            var query = DbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery)

        {
            var query = DbSet.Where(predicate);

            query = includeQuery(query);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<Expression<Func<TEntity, BaseEntity>>> includes)
        {
            var query = DbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery)
        {
            var query = DbSet.Where(predicate);

            query = includeQuery(query);

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort)
        {
            var query = DbSet.Where(predicate);

            query = sort(query);

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort, IEnumerable<Expression<Func<TEntity, BaseEntity>>> includes)
        {
            var query = DbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = sort(query);

            return await query.ToListAsync();
        }

        public async Task<PaginationResponse<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort, int pageIndex, int pageSize)
        {
            var query = sort(DbSet.Where(predicate));

            return await this.GetPaginationEntities(query, pageIndex, pageSize);
        }

        public async Task<PaginationResponse<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
            int pageIndex,
            int pageSize,
            IEnumerable<Expression<Func<TEntity, BaseEntity>>> includes)
        {
            var query = DbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = sort(query);

            return await this.GetPaginationEntities(query, pageIndex, pageSize);
        }

        public async Task<PaginationResponse<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
            int pageIndex,
            int pageSize,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery)
        {
            var query = DbSet.Where(predicate);

            query = includeQuery(query);

            query = sort(query);

            return await this.GetPaginationEntities(query, pageIndex, pageSize);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = (await DbSet.AddAsync(entity)).Entity;
            await Context.SaveChangesAsync();

            return addedEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var removedEntity = DbSet.Remove(entity).Entity;
            await Context.SaveChangesAsync();

            return removedEntity;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }

        public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities)
        {
            DbSet.UpdateRange(entities);

            await Context.SaveChangesAsync();

            return entities;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);

            await Context.SaveChangesAsync();

            return entities;
        }

        protected async Task<PaginationResponse<TEntity>> GetPaginationEntities(IQueryable<TEntity> query, int pageIndex, int pageSize)
        {
            var totalItems = await query.CountAsync();

            var response = new PaginationResponse<TEntity>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalItems,
                Items = Enumerable.Empty<TEntity>()
            };

            if (totalItems > 0)
            {
                response.Items = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            }

            return response;
        }
    }
}
