using System.Net;
using System.Diagnostics.CodeAnalysis;
using Polly.Extensions.Http;
using Polly;

namespace EmailSender.Backend.Configuration;

[ExcludeFromCodeCoverage]
public static class PollyPolicyHandler
{
    public static IAsyncPolicy<HttpResponseMessage> SetupRetry()
    {
        const int retryCount = 3;
        const double durationBetweenRetries = 150;

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TaskCanceledException>()
            .OrResult(response => response.StatusCode 
                is HttpStatusCode.RequestTimeout 
                or HttpStatusCode.BadGateway 
                or HttpStatusCode.GatewayTimeout 
                or HttpStatusCode.ServiceUnavailable
            ).WaitAndRetryAsync(retryCount, count 
                => TimeSpan.FromMilliseconds(durationBetweenRetries * Math.Pow(2, count - 1)));
    }
}