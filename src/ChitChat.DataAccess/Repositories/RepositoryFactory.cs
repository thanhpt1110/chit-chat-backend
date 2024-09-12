using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.DataAccess.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
            => this._serviceProvider.GetRequiredService<IBaseRepository<TEntity>>();
    }
}
