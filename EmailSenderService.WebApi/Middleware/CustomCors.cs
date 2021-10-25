namespace EmailSenderService.WebApi.Middleware
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Backend.EmailService.Services;

    [ExcludeFromCodeCoverage]
    public class CustomCors
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomCors(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task Invoke(HttpContext httpContext, ISenderService senderService)
        {
            var requestOrigin = httpContext.Request.Headers["Origin"];
            var allowDomains = await senderService.IsDomainAllowed(requestOrigin, CancellationToken.None);
            
            if (!allowDomains)
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync("Forbidden");
                return;
            }

            await _requestDelegate(httpContext);
        }
    }
}