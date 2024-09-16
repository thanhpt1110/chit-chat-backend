using ChitChat.Application.Helpers;
using ChitChat.DataAccess.Data;
using ChitChat.Domain.Identity;
using ChitChat.Infrastructure.Caching;
using ChitChat.Infrastructure.EntityFrameworkCore;
using ChitChat.Infrastructure.Logging;
using ChitChat.Infrastructure.Middleware;
using ChitChat.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Infrastructure
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder
            .AddEntityFramewordCore()
            //.AddAppAuthorization()
            .AddCaching();


            // Services
            builder.Services
             .AddInfrastructureService().AddAuthorization();

            // Host
            builder.Host.AddHostSerilogConfiguration();
            return builder;
        }
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddIdentity();
            services.AddSingleton<IClaimService, ClaimService>();
            services.AddSingleton<ITokenService, TokenService>();
            return services;
        }
        public static IApplicationBuilder AddInfrastuctureApplication(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            return app;
        }
        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<UserApplication, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });
        }
    }
}
