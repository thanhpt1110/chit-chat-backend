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
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var connectionString = environment == "Development"
                    ? configuration.GetSection("Database:ConnectionStrings").GetValue<string>("LocalConnection")
                    : configuration.GetSection("Database:ConnectionStrings").GetValue<string>("AWSConnection");

                options
                    .AddInterceptors(provider.GetRequiredService<ContextSaveChangeInterceptor>())
                    .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 39)),
                        opt =>
                        {
                            opt.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                            opt.EnableRetryOnFailure();
                        });
            });

            builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return builder;
        }
    }
}
