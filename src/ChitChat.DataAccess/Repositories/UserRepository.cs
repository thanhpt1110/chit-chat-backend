using System.Linq.Expressions;

using ChitChat.DataAccess.Data;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.Domain.Entities;
using ChitChat.Domain.Exceptions;
using ChitChat.Domain.Identity;

using Microsoft.EntityFrameworkCore.Query;

namespace ChitChat.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<UserApplication> DbSet;
        public UserRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<UserApplication>();
        }
        public IQueryable<UserApplication> GetQuery() => this.DbSet;

        public async Task<UserApplication> GetFirstAsync(Expression<Func<UserApplication, bool>> predicate)
        {
            var entity = await DbSet.Where(predicate).FirstOrDefaultAsync();
            return entity ?? throw new ResourceNotFoundException(typeof(UserApplication));
        }

        public async Task<UserApplication> GetFirstOrDefaultAsync(Expression<Func<UserApplication, bool>> predicate)
        {
            return await DbSet.Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<List<UserApplication>> GetAllAsync(Expression<Func<UserApplication, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<UserApplication>> GetAllAsync(Expression<Func<UserApplication, bool>> predicate, IEnumerable<Expression<Func<UserApplication, BaseEntity>>> includes)
        {
            var query = DbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<List<UserApplication>> GetAllAsync(Expression<Func<UserApplication, bool>> predicate, Func<IQueryable<UserApplication>, IIncludableQueryable<UserApplication, object>> includeQuery)
        {
            var query = DbSet.Where(predicate);

            query = includeQuery(query);

            return await query.ToListAsync();
        }

        public async Task<List<UserApplication>> GetAllAsync(Expression<Func<UserApplication, bool>> predicate, Func<IQueryable<UserApplication>, IOrderedQueryable<UserApplication>> sort)
        {
            var query = DbSet.Where(predicate);

            query = sort(query);

            return await query.ToListAsync();
        }

        public async Task<UserApplication> UpdateAsync(UserApplication entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<UserApplication> DeleteAsync(UserApplication entity)
        {
            var removedEntity = DbSet.Remove(entity).Entity;
            await Context.SaveChangesAsync();

            return removedEntity;
        }

        public async Task<bool> AnyAsync(Expression<Func<UserApplication, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }

        public async Task<List<UserApplication>> UpdateRangeAsync(List<UserApplication> entities)
        {
            DbSet.UpdateRange(entities);

            await Context.SaveChangesAsync();

            return entities;
        }



    }
}
