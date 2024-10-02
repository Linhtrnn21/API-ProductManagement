using Microsoft.Extensions.Logging;

namespace PM.Helper
{
    public class LoggingHelper : ILoggingHelper
    {
        private readonly ILogger<LoggingHelper> _logger;


        public LoggingHelper(ILogger<LoggingHelper> logger)
        {
            _logger = logger;
        }

        public void LogUserRequest(string userId, string action)
        {
            _logger.LogInformation($"User {userId} is requesting {action} at {DateTime.UtcNow}");
        }

        public void LogSuccessfulRequest(string userId, string action, int itemCount)
        {
            _logger.LogInformation($"Successfully retrieved {itemCount} items for user {userId} for action: {action}");
        }

        public void LogCacheHit(string cacheKey, string userId)
        {
            _logger.LogInformation($"Cache hit for key '{cacheKey}' for user {userId} at {DateTime.UtcNow}");
        }
    }
}
