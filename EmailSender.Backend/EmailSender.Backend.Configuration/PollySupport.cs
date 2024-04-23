using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSender.Backend.Configuration;

[ExcludeFromCodeCoverage]
public static class PollySupport
{
    public static void SetupRetryPolicyWithPolly(IServiceCollection services)
    {
        services.AddHttpClient("RetryHttpClient", options =>
        {
            options.DefaultRequestHeaders.Add("Accept", "application/json");
            options.Timeout = TimeSpan.FromMinutes(5);
            options.DefaultRequestHeaders.ConnectionClose = true;
        }).AddPolicyHandler(PollyPolicyHandler.SetupRetry());
    }
}