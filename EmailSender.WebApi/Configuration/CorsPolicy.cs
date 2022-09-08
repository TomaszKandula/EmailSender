using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;

namespace EmailSender.WebApi.Configuration;

[ExcludeFromCodeCoverage]
public static class CorsPolicy
{
    /// <summary>
    /// Allows to configure CORS policy for any incoming origin without credentials.
    /// </summary>
    /// <remarks>
    /// Use this only when origins are filtered by the separate middleware.
    /// This would be the case for list of allowed domains that are taken from
    /// database on each request (the list may be changes frequently and will not
    /// require to restart application to reload domain list).  
    /// </remarks>
    /// <param name="builder">Application Builder instance.</param>
    public static void ApplyCorsPolicy(this IApplicationBuilder builder)
    {
        builder.UseCors(policyBuilder =>
        {
            policyBuilder
                .AllowAnyOrigin()
                .WithHeaders(
                    HeaderNames.Accept,
                    HeaderNames.ContentType,
                    HeaderNames.XRequestedWith,
                    HeaderNames.AccessControlAllowOrigin,
                    HeaderNames.AccessControlAllowHeaders, 
                    HeaderNames.AccessControlAllowMethods,
                    HeaderNames.AccessControlMaxAge)
                .WithMethods("GET", "POST")
                .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
        });            
    }
}