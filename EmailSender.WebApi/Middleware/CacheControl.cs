using System.Diagnostics.CodeAnalysis;
using Microsoft.Net.Http.Headers;

namespace EmailSender.WebApi.Middleware;

[ExcludeFromCodeCoverage]
public class CacheControl
{
    private readonly RequestDelegate _requestDelegate;

    public CacheControl(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        httpContext.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
        {
            NoStore = true,
            NoCache = true
        };

        await _requestDelegate(httpContext);
    }
}