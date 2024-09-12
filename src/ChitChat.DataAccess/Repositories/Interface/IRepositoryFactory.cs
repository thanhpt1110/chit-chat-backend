using ChitChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.DataAccess.Repositories.Interrface
{
    public interface IRepositoryFactory
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }

}
