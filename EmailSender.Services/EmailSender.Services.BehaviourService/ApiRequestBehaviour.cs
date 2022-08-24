namespace EmailSender.Services.BehaviourService;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using UserService;
using Backend.Shared.Attributes;
using Backend.Core.Services.LoggerService;
using MediatR;

[ExcludeFromCodeCoverage]
public class ApiRequestBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILoggerService _logger;

    private readonly IUserService _userService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiRequestBehaviour(ILoggerService logger, IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var endpoint = _httpContextAccessor.HttpContext?.Features.Get<IEndpointFeature>()?.Endpoint;
        var isBillable = endpoint?.Metadata.Any(@object => @object is BillableEndpointAttribute) ?? false;

        if (!isBillable) return await next();

        var userId = await _userService.RegisterUserApiRequest(typeof(TRequest).Name, cancellationToken);
        _logger.LogInformation($"Billable API request has been logged with the system. User ID: {userId}");
        return await next();
    }
}