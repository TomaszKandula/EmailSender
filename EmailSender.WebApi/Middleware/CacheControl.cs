using System.Diagnostics.CodeAnalysis;
using Microsoft.Net.Http.Headers;

namespace EmailSender.WebApi.Middleware;

/// <summary>
/// Cache control.
/// </summary>
[ExcludeFromCodeCoverage]
public class CacheControl
{
    private readonly RequestDelegate _requestDelegate;

    /// <summary>
    /// Cache control.
    /// </summary>
    /// <param name="requestDelegate">Delegate instance.</param>
    public CacheControl(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

    /// <summary>
    /// Invoke middleware.
    /// </summary>
    /// <param name="httpContext">Current HTTP context.</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var headerValue = new CacheControlHeaderValue { NoStore = true, NoCache = true };
        httpContext.Response.GetTypedHeaders().CacheControl = headerValue;
        await _requestDelegate(httpContext);
    }
}