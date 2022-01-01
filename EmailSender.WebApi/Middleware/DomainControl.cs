namespace EmailSender.WebApi.Middleware;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Services.UserService;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;

[ExcludeFromCodeCoverage]
public class DomainControl
{
    private readonly RequestDelegate _requestDelegate;

    public DomainControl(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

    public async Task InvokeAsync(HttpContext httpContext, IUserService userService)
    {
        var origin = httpContext.Request.Host.ToString();
        var isDomainAllowed = await userService.IsDomainAllowed(origin, CancellationToken.None);

        if (!isDomainAllowed)
            throw new AccessException(nameof(ErrorCodes.ACCESS_FORBIDDEN), ErrorCodes.ACCESS_FORBIDDEN);

        await _requestDelegate(httpContext);
    }
}