namespace PM.Helper.Interface
{
    public interface ICachingHelper
    {
        Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem);
        void Remove(string cacheKey);
    }
}
