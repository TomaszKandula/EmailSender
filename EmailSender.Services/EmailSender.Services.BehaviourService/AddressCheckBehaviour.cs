using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Core.Services.LoggerService;
using EmailSender.Backend.Shared.Resources;
using EmailSender.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace EmailSender.Services.BehaviourService;

[ExcludeFromCodeCoverage]
public class AddressCheckBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private const string Localhost = "127.0.0.1";

    private const string XForwardedFor = "X-Forwarded-For";

    private readonly ILoggerService _logger;

    private readonly IUserService _userService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddressCheckBehaviour(ILoggerService logger, IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var endpoint = _httpContextAccessor.HttpContext?.Features.Get<IEndpointFeature>()?.Endpoint;
        var allowAnonymous = endpoint?.Metadata.Any(@object => @object is AllowAnonymousAttribute) ?? false;
        if (allowAnonymous)
            return await next();

        var ipAddress = GetRequestIpAddress();
        var isIpAddressAllowed = await _userService.IsIpAddressAllowed(ipAddress, CancellationToken.None);

        if (isIpAddressAllowed) 
            return await next();

        _logger.LogWarning($"Access forbidden for: {ipAddress}");
        throw new Backend.Core.Exceptions.AccessException(nameof(ErrorCodes.ACCESS_FORBIDDEN), ErrorCodes.ACCESS_FORBIDDEN);
    }

    private string GetRequestIpAddress() 
    {
        var remoteIpAddress = _httpContextAccessor.HttpContext?
            .Request.Headers[XForwardedFor].ToString();

        return string.IsNullOrEmpty(remoteIpAddress) 
            ? Localhost 
            : remoteIpAddress.Split(':')[0];
    }
}