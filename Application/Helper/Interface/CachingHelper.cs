using Microsoft.Extensions.Caching.Memory;

namespace PM.Helper.Interface
{
    public class CachingHelper : ICachingHelper
    {
        private readonly IMemoryCache _cache;

        public CachingHelper(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem)
        {
            if (!_cache.TryGetValue(cacheKey, out T cachedItem))
            {
                cachedItem = await createItem();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _cache.Set(cacheKey, cachedItem, cacheOptions);
            }

            return cachedItem;
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }
    }
}
