namespace EmailSender.Backend.Cqrs.Handlers.Queries.Smtp;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Services.SenderService;
using Core.Services.LoggerService;

public class GetServerStatusQueryHandler : Cqrs.RequestHandler<GetServerStatusQuery, Unit>
{
    private readonly IUserService _userService;
        
    private readonly ISenderService _senderService;

    private readonly ILoggerService _loggerService;

    public GetServerStatusQueryHandler(IUserService userService, ISenderService senderService, ILoggerService loggerService)
    {
        _userService = userService;
        _senderService = senderService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(GetServerStatusQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        var emailId = await _senderService.VerifyEmailFrom(request.EmailAddress, userId, cancellationToken);
        VerifyArguments(userId, emailId);

        await _senderService.VerifyConnection(emailId, cancellationToken);
        _loggerService.LogInformation($"Connection has been verified. User ID: {userId}. Email ID: {emailId}");

        return Unit.Value;
    }

    private static void VerifyArguments(Guid? userId, Guid? emailId)
    {
        if (userId == null || userId == Guid.Empty)
        {
            var message = $"Cannot verify SMTP service. Reason: {ErrorCodes.INVALID_ASSOCIATED_USER}";
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), message);
        }

        if (emailId == null || emailId == Guid.Empty)
        {
            var message = $"Cannot verify SMTP service. Reason: {ErrorCodes.INVALID_ASSOCIATED_EMAIL}";
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL), message);
        }
    }
}