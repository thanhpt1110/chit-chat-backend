using ChitChat.Domain.Entities;

namespace ChitChat.DataAccess.Repositories.Interrface
{
    public interface IRepositoryFactory
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }

}
