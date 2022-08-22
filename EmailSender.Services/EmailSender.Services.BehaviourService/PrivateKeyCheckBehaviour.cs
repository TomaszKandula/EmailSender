namespace EmailSender.Services.BehaviourService;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using UserService;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;
using Backend.Core.Services.LoggerService;
using MediatR;

[ExcludeFromCodeCoverage]
public class PrivateKeyCheckBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILoggerService _logger;

    private readonly IUserService _userService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public PrivateKeyCheckBehaviour(ILoggerService logger, IUserService userService, IHttpContextAccessor httpContextAccessor)
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

        var privateKeyFromHeader = _userService.GetPrivateKeyFromHeader();

        if (string.IsNullOrEmpty(privateKeyFromHeader))
        {
            _logger.LogWarning("Missing private key in the request header. Access denied.");
            throw new AccessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);
        }

        var isKeyValid = await _userService.IsPrivateKeyValid(privateKeyFromHeader, cancellationToken);
        var userId = await _userService.GetUserByPrivateKey(privateKeyFromHeader, cancellationToken);

        if (!isKeyValid)
        {
            _logger.LogWarning("Provided private key in the request header is invalid. Access denied.");
            throw new AccessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);
        }

        if (userId == Guid.Empty)
        {
            _logger.LogWarning("User ID for given private key is null or empty. Access denied.");
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);
        }

        return await next();
    }
}