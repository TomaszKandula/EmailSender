using EmailSender.Backend.Core.Utilities.LoggerService;

namespace EmailSender.Services.HttpClientService.Abstractions;

public interface IHttpClientServiceFactory
{
    IHttpClientService Create(bool allowAutoRedirect, ILoggerService loggerService);
}