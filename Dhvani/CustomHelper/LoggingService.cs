namespace Dhvani.CustomHelper
{
    public class LoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public void LogRequestInformation(HttpRequest request)
        {
            _logger.LogInformation($"Request received: {request.Method} {request.Path}");
            // Log other relevant information...
        }
    }
}