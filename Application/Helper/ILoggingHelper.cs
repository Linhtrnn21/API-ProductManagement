namespace PM.Helper
{
    public interface ILoggingHelper
    {
        void LogUserRequest(string userId, string action);
        void LogSuccessfulRequest(string userId, string action, int itemCount);
        void LogFailedRequest(string userId, string message);
        void LogCacheHit(string cacheKey, string userId);
    }
}
