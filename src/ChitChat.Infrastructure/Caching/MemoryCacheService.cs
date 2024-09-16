using ChitChat.Application.Services.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Infrastructure.Caching
{
    public class MemoryCacheService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;

        private MemoryCacheEntryOptions MemoryCacheEntryOptions = new()
        {
            AbsoluteExpirationRelativeToNow = CacheEntryOptions.Default.AbsoluteExpirationRelativeToNow,
            SlidingExpiration = CacheEntryOptions.Default.SlidingExpiration
        };

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public async Task Set<T>(string key, T value)
        {
            await Task.Yield();
            this._memoryCache.Set(key, value, this.MemoryCacheEntryOptions);
        }

        public async Task<T> Get<T>(string key)
        {
            await Task.Yield();
            this._memoryCache.TryGetValue<T>(key, out var cacheEntry);
            return cacheEntry;
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func)
        {
            var cacheEntry = await this._memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry
                  .SetSlidingExpiration(this.MemoryCacheEntryOptions.SlidingExpiration!.Value)
                  .SetAbsoluteExpiration(this.MemoryCacheEntryOptions.AbsoluteExpirationRelativeToNow!.Value);

                return await func();
            });

            return cacheEntry;
        }

        public async Task Remove(string key)
        {
            this._memoryCache.Remove(key);
            await Task.CompletedTask;
        }
    }

}
