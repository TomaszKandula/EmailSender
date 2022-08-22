namespace EmailSender.Services.BehaviourService;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using UserService;
using Backend.Shared.Resources;
using Backend.Shared.Attributes;
using Backend.Core.Services.LoggerService;
using MediatR;

[ExcludeFromCodeCoverage]
public class DomainCheckBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILoggerService _logger;

    private readonly IUserService _userService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public DomainCheckBehaviour(ILoggerService logger, IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var endpoint = _httpContextAccessor.HttpContext?.Features.Get<IEndpointFeature>()?.Endpoint;
        var shouldSkipCheck = endpoint?.Metadata.Any(@object => @object is SkipIpAddressCheckAttribute) ?? false;
        if (shouldSkipCheck)
            return await next();

        var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4();
        var isIpAddressAllowed = await _userService.IsIpAddressAllowed(ipAddress, CancellationToken.None);

        if (isIpAddressAllowed) 
            return await next();

        _logger.LogWarning($"Access forbidden for: {ipAddress}");
        throw new Backend.Core.Exceptions.AccessException(nameof(ErrorCodes.ACCESS_FORBIDDEN), ErrorCodes.ACCESS_FORBIDDEN);
    }
}