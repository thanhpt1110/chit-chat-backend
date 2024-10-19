using ChitChat.DataAccess;
using ChitChat.DataAccess.Data;
using ChitChat.DataAccess.Data.Interceptor;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChitChat.Infrastructure.EntityFrameworkCore
{
    public static class EntityFrameworkRegisteration
    {
        public static WebApplicationBuilder AddEntityFramewordCore(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            builder.Services.AddScoped<ContextSaveChangeInterceptor>();

            builder.Services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var databaseConfig = configuration.GetRequiredSection("Database").Get<DatabaseConfiguration>();

                options
                    .AddInterceptors(provider.GetRequiredService<ContextSaveChangeInterceptor>());


                options.UseSqlServer(databaseConfig.ConnectionString, opt =>
                {
                    opt.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });

            });

            builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return builder;
        }

    }
}
