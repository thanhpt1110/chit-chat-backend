using ChitChat.DataAccess.Repositories;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.DataAccess.Repositories.Interrface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            .AddScoped<IRepositoryFactory, RepositoryFactory>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IConversationRepository, ConversationRepository>();
        }

    }
}
