using MediatR;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Core.Utilities.DateTimeService;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Backend.Domain.Entities;
using EmailSender.Backend.Shared.Resources;
using EmailSender.Persistence.Database;
using EmailSender.Services.SenderService;
using EmailSender.Services.SenderService.Models;
using EmailSender.Services.UserService;

namespace EmailSender.Backend.Application.Emails;

public class SendEmailCommandHandler : RequestHandler<SendEmailCommand, Unit>
{
    private readonly DatabaseContext _databaseContext;

    private readonly IUserService _userService;

    private readonly ISenderService _senderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly ILoggerService _loggerService;

    public SendEmailCommandHandler(DatabaseContext databaseContext, IUserService userService,
        ISenderService senderService, IDateTimeService dateTimeService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _senderService = senderService;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var emailId = await _senderService.VerifyEmailFrom(request.From, userId, cancellationToken);
        if (emailId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL), ErrorCodes.INVALID_ASSOCIATED_EMAIL);

        var configuration = new Configuration
        {
            From = request.From,
            Subject = request.Subject,
            To = request.To,
            Cc = request.Cc ?? new List<string>(),
            Bcc = request.Bcc ?? new List<string>(),
            Body = request.Body,
            IsHtml = request.IsHtml
        };

        await _senderService.Send(configuration, cancellationToken);
        _loggerService.LogInformation($"Email has been successfully sent from: {request.From}");

        var history = new SentHistory
        {
            UserId = userId,
            EmailId = emailId,
            SentAt = _dateTimeService.Now
        };

        await _databaseContext.SentHistory.AddAsync(history, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        _loggerService.LogInformation($"Email history updated. User ID {userId}. Email ID {emailId}");
            
        return Unit.Value;
    }
}