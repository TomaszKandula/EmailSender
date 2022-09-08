using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Core.Services.LoggerService;
using EmailSender.Backend.Shared.Resources;
using EmailSender.Services.SenderService;
using EmailSender.Services.UserService;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Emails;

public class VerifyEmailCommandHandler : RequestHandler<VerifyEmailCommand, VerifyEmailCommandResult>
{
    private readonly IUserService _userService;

    private readonly ISenderService _senderService;

    private readonly ILoggerService _loggerService;

    public VerifyEmailCommandHandler(IUserService userService, ISenderService senderService, ILoggerService loggerService)
    {
        _userService = userService;
        _senderService = senderService;
        _loggerService = loggerService;
    }

    public override async Task<VerifyEmailCommandResult> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var result = await _senderService.VerifyEmailAddress(request.Emails, cancellationToken);
        _loggerService.LogInformation($"Emails verified. Count: {request.Emails.Count()}");

        return new VerifyEmailCommandResult
        {
            CheckResult = result
        };
    }
}