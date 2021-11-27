namespace EmailSender.WebApi.Middleware
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Backend.UserService;
    using Backend.Core.Exceptions;
    using Backend.Shared.Resources;

    [ExcludeFromCodeCoverage]
    public class DomainControl
    {
        private readonly RequestDelegate _requestDelegate;

        public DomainControl(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task Invoke(HttpContext httpContext, IUserService userService)
        {
            var origin = httpContext.Request.Host.ToString();
            var allowDomains = await userService.IsDomainAllowed(origin, CancellationToken.None);

            if (!allowDomains)
                throw new AccessException(nameof(ErrorCodes.ACCESS_FORBIDDEN), ErrorCodes.ACCESS_FORBIDDEN);

            await _requestDelegate(httpContext);
        }
    }
}