using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Core.Utilities.LoggerService;
using MediatR;

namespace EmailSender.Services.BehaviourService;

[ExcludeFromCodeCoverage]
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILoggerService _logger;

    public LoggingBehaviour(ILoggerService logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation($"Begin: Handle {typeof(TRequest).Name}");
        var response = await next();
        _logger.LogInformation($"Finish: Handle {typeof(TResponse).Name}");
        return response;
    }
}