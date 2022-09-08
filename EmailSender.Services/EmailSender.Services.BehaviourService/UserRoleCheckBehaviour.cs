using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Backend.Domain.Enums;
using EmailSender.Backend.Shared.Attributes;
using EmailSender.Backend.Shared.Resources;
using EmailSender.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using MediatR;

namespace EmailSender.Services.BehaviourService;

[ExcludeFromCodeCoverage]
public class UserRoleCheckBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILoggerService _loggerService;

    private readonly IUserService _userService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRoleCheckBehaviour(ILoggerService loggerService, IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _loggerService = loggerService;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var endpoint = _httpContextAccessor.HttpContext?.Features.Get<IEndpointFeature>()?.Endpoint;
        var requireAdmin = endpoint?.Metadata.Any(@object => @object is RequireAdministratorAttribute) ?? false;
        if (!requireAdmin) return await next();

        var userRole = await _userService.GetUserRoleByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userRole == UserRole.Administrator) return await next();

        _loggerService.LogWarning($"Access forbidden for user role: '{userRole}'");
        throw new AccessException(nameof(ErrorCodes.ACCESS_FORBIDDEN), ErrorCodes.ACCESS_FORBIDDEN);
    }
}