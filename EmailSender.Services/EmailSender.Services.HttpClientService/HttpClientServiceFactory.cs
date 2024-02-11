using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Services.HttpClientService.Abstractions;

namespace EmailSender.Services.HttpClientService;

public class HttpClientServiceFactory : IHttpClientServiceFactory
{
    public IHttpClientService Create(bool allowAutoRedirect, ILoggerService loggerService)
    {
        var handler = new HttpClientHandler { AllowAutoRedirect = allowAutoRedirect }; 
        var client = new HttpClient(handler);
        return new HttpClientService(client, loggerService);
    }
}