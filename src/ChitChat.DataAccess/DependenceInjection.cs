using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChitChat.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using ChitChat.DataAccess.Data;
using Microsoft.Extensions.Options;

namespace ChitChat.DataAccess
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddDataAccessService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddRepositories();
            return services;
        }
        private static void AddRepositories(this IServiceCollection services)
        {
            services
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddScoped<IRepositoryFactory, RepositoryFactory>();
        }

    }
}
