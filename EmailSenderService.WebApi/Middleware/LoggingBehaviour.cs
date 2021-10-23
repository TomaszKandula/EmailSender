namespace EmailSenderService.WebApi.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Backend.Shared.Services.Logger;

    public class LoggingBehaviour
    {
        private readonly RequestDelegate _requestDelegate;
        
        public LoggingBehaviour(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task Invoke(HttpContext httpContext, ILogger logger)
        {
            logger.LogInformation($"Begin handle request: '{httpContext.Request.Path}'.");
            await _requestDelegate(httpContext);
            logger.LogInformation($"Finish handle request: '{httpContext.Request.Path}'.");
        }
    }
}