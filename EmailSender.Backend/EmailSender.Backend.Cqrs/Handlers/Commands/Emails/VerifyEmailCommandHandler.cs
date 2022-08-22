namespace EmailSender.Backend.Cqrs.Handlers.Commands.Emails;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Domain.Entities;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Services.SenderService;
using Core.Services.LoggerService;
using Core.Services.DateTimeService;

public class VerifyEmailCommandHandler : RequestHandler<VerifyEmailCommand, VerifyEmailCommandResult>
{
    private readonly DatabaseContext _databaseContext;

    private readonly IUserService _userService;

    private readonly ISenderService _senderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly ILoggerService _loggerService;

    public VerifyEmailCommandHandler(DatabaseContext databaseContext, IUserService userService,
        ISenderService senderService, IDateTimeService dateTimeService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _senderService = senderService;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
    }

    public override async Task<VerifyEmailCommandResult> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var apiRequest = new RequestsHistory
        {
            UserId = userId,
            Requested = _dateTimeService.Now,
            RequestName = nameof(VerifyEmailCommand)
        };

        await _databaseContext.AddAsync(apiRequest, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        _loggerService.LogInformation($"Request has been logged with the system. User ID: {userId}");

        var result = await _senderService.VerifyEmailAddress(request.Emails, cancellationToken);
        _loggerService.LogInformation($"Emails verified. Count: {request.Emails.Count()}");

        return new VerifyEmailCommandResult
        {
            CheckResult = result
        };
    }
}