namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Core.Services.LoggerService;
using Services.UserService.Models;
using MediatR;

public class UpdateUserEmailCommandHandler : Cqrs.RequestHandler<UpdateUserEmailCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public UpdateUserEmailCommandHandler(IUserService userService, ILoggerService loggerService)
    {
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var input = new UpdateUserEmailInput
        {
            UserId = userId,
            NewEmailId = request.NewEmailId,
            OldEmailId = request.OldEmailId
        };

        await _userService.UpdateUserEmail(input, cancellationToken);
        _loggerService.LogInformation($"User email has been replaced, user ID: {userId}");

        return Unit.Value;
    }
}