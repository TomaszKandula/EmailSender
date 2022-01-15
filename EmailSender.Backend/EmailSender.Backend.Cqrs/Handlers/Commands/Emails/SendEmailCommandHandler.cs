namespace EmailSender.Backend.Cqrs.Handlers.Commands.Emails;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Core.Exceptions;
using Domain.Entities;
using Shared.Resources;
using Services.UserService;
using Services.SenderService;
using Core.Services.LoggerService;
using Services.SenderService.Models;
using Core.Services.DateTimeService;

public class SendEmailCommandHandler : Cqrs.RequestHandler<SendEmailCommand, Unit>
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
        var emailId = await _senderService.VerifyEmailFrom(request.From, userId, cancellationToken);

        if (emailId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL), ErrorCodes.INVALID_ASSOCIATED_EMAIL);

        var apiRequest = new RequestsHistory
        {
            UserId = userId,
            Requested = _dateTimeService.Now,
            RequestName = nameof(SendEmailCommand)
        };

        await _databaseContext.AddAsync(apiRequest, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        _loggerService.LogInformation($"Request has been logged with the system. User ID: {userId}");

        var configuration = new Configuration
        {
            From = request.From,
            Subject = request.Subject,
            To = request.To,
            Cc = request.Cc,
            Bcc = request.Bcc,
            Body = request.Body,
            IsHtml = request.IsHtml
        };

        await _senderService.Send(configuration, cancellationToken);
        _loggerService.LogInformation($"Email has been successfully sent from: {request.From}");

        var history = new EmailsHistory
        {
            UserId = userId,
            EmailId = emailId,
            Sent = _dateTimeService.Now
        };

        await _databaseContext.EmailsHistory.AddAsync(history, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        _loggerService.LogInformation($"Email history updated. User ID {userId}. Email ID {emailId}");
            
        return Unit.Value;
    }
}