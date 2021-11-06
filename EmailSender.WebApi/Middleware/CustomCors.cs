﻿namespace EmailSender.WebApi.Middleware
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Configuration;
    using Backend.EmailService.Services.SenderService;

    [ExcludeFromCodeCoverage]
    public class CustomCors
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomCors(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task Invoke(HttpContext httpContext, ISenderService senderService)
        {
            var origin = httpContext.Request.Host.ToString();
            var allowDomains = await senderService.IsDomainAllowed(origin, CancellationToken.None);

            if (!allowDomains)
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync("Forbidden");
                return;
            }

            CorsHeaders.Apply(httpContext);
            await _requestDelegate(httpContext);
        }
    }
}