using ChitChat.Application.Services.Caching;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Infrastructure.Caching
{
    public static class CachingRegistrations
    {
        public static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddMemoryCache()
                .AddSingleton<ICachingService, MemoryCacheService>();
            return builder;
        }
    }

}
