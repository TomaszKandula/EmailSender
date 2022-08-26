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

public class RemoveUserEmailCommandHandler : Cqrs.RequestHandler<RemoveUserEmailCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public RemoveUserEmailCommandHandler(IUserService userService, ILoggerService loggerService)
    {
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(RemoveUserEmailCommand request, CancellationToken cancellationToken)
    {
        var input = new RemoveUserEmailInput
        {
            UserId = request.UserId,
            EmailId = request.EmailId
        };

        await _userService.RemoveUserEmail(input, cancellationToken);
        _loggerService.LogInformation($"User email has been removed, user ID: {request.UserId}");

        return Unit.Value;
    }
}