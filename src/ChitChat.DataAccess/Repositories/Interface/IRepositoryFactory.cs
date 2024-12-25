using ChitChat.Domain.Entities;

namespace ChitChat.DataAccess.Repositories.Interface
{
    public interface IRepositoryFactory
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }

}
