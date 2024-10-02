namespace PM.Helper
{
    public interface ILoggingHelper
    {
        void LogUserRequest(string userId, string action);
        void LogSuccessfulRequest(string userId, string action, int itemCount);
        void LogCacheHit(string cacheKey, string userId);
    }
}
