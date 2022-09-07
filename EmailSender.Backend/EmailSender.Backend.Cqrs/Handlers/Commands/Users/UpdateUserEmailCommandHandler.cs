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
        var input = new UpdateUserEmailInput
        {
            UserId = request.UserId,
            NewEmailId = request.NewEmailId,
            OldEmailId = request.OldEmailId
        };

        await _userService.UpdateUserEmail(input, cancellationToken);
        _loggerService.LogInformation($"User email has been replaced, user ID: {request.UserId}");

        return Unit.Value;
    }
}