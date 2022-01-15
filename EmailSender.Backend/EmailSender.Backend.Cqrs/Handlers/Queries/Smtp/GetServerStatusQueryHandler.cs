namespace EmailSender.Backend.Cqrs.Handlers.Queries.Smtp;

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
using Core.Services.DateTimeService;

public class GetServerStatusQueryHandler : Cqrs.RequestHandler<GetServerStatusQuery, Unit>
{
    private readonly DatabaseContext _databaseContext;

    private readonly IUserService _userService;
        
    private readonly ISenderService _senderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly ILoggerService _loggerService;

    public GetServerStatusQueryHandler(DatabaseContext databaseContext, IUserService userService, 
        ISenderService senderService, IDateTimeService dateTimeService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _senderService = senderService;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(GetServerStatusQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        var emailId = await _senderService.VerifyEmailFrom(request.EmailAddress, userId, cancellationToken);

        VerifyArguments(userId, emailId);

        var apiRequest = new RequestsHistory
        {
            UserId = userId,
            Requested = _dateTimeService.Now,
            RequestName = nameof(GetServerStatusQuery)
        };

        await _databaseContext.AddAsync(apiRequest, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        _loggerService.LogInformation($"Request has been logged with the system. User ID: {userId}");

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