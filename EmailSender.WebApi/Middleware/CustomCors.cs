namespace EmailSender.WebApi.Middleware
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Backend.UserService;
    using Backend.Core.Models;
    using Backend.Shared.Resources;
    using Newtonsoft.Json;

    [ExcludeFromCodeCoverage]
    public class CustomCors
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomCors(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task Invoke(HttpContext httpContext, IUserService userService)
        {
            var origin = httpContext.Request.Host.ToString();
            var allowDomains = await userService.IsDomainAllowed(origin, CancellationToken.None);

            if (!allowDomains)
            {
                httpContext.Response.StatusCode = 403;
                var applicationError = new ApplicationError(nameof(ErrorCodes.ACCESS_FORBIDDEN), ErrorCodes.ACCESS_FORBIDDEN);
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(applicationError));
                return;
            }

            await _requestDelegate(httpContext);
        }
    }
}