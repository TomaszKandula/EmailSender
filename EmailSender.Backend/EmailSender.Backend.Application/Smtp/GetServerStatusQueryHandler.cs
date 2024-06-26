using MediatR;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Backend.Shared.Resources;
using EmailSender.Services.SenderService;
using EmailSender.Services.UserService;

namespace EmailSender.Backend.Application.Smtp;

public class GetServerStatusQueryHandler : RequestHandler<GetServerStatusQuery, Unit>
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