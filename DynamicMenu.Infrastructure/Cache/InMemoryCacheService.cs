using DynamicMenu.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace DynamicMenu.Infrastructure.Cache
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            var options = new MemoryCacheEntryOptions();
            if (expirationTime.HasValue)
                options.AbsoluteExpirationRelativeToNow = expirationTime;
            else
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

            _cache.Set(key, value, options);
        }

        public async Task RemoveAsync(string key)
        {
            _cache.Remove(key);
        }
    }
} 