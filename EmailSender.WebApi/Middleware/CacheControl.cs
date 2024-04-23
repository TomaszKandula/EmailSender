using System.Diagnostics.CodeAnalysis;
using Microsoft.Net.Http.Headers;

namespace EmailSender.WebApi.Middleware;

/// <summary>
/// 
/// </summary>
[ExcludeFromCodeCoverage]
public class CacheControl
{
    private readonly RequestDelegate _requestDelegate;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDelegate"></param>
    public CacheControl(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContext"></param>
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