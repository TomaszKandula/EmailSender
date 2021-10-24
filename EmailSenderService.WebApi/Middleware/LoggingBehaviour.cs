namespace EmailSenderService.WebApi.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Backend.Shared.Services.LoggerService;

    public class LoggingBehaviour
    {
        private readonly RequestDelegate _requestDelegate;

        public LoggingBehaviour(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task Invoke(HttpContext httpContext, ILoggerService logger)
        {
            var requestedPath = $"{httpContext.Request.Path}";
            var requestedBy = GetRequestIpAddress(httpContext);
            logger.LogInformation($"Handling request to '{requestedPath}' from remote IP address: '{requestedBy}'.");
            await _requestDelegate(httpContext);
        }

        private static string GetRequestIpAddress(HttpContext httpContext) 
        {
            var remoteIpAddress = httpContext.Request.Headers["X-Forwarded-For"].ToString();
            return string.IsNullOrEmpty(remoteIpAddress) 
                ? "127.0.0.1" 
                : remoteIpAddress.Split(':')[0];
        }
    }
}