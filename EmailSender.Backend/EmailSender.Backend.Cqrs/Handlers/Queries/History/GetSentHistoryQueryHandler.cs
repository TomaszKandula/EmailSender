namespace EmailSender.Backend.Cqrs.Handlers.Queries.History;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Shared.Models;
using Core.Exceptions;
using Domain.Entities;
using Shared.Resources;
using Services.UserService;
using Core.Services.LoggerService;
using Core.Services.DateTimeService;

public class GetSentHistoryQueryHandler : RequestHandler<GetSentHistoryQuery, GetSentHistoryQueryResult>
{
    private readonly DatabaseContext _databaseContext;
        
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly ILoggerService _loggerService;

    public GetSentHistoryQueryHandler(DatabaseContext databaseContext, IUserService userService, 
        IDateTimeService dateTimeService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
    }

    public override async Task<GetSentHistoryQueryResult> Handle(GetSentHistoryQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var apiRequest = new RequestsHistory
        {
            UserId = userId,
            RequestedAt = _dateTimeService.Now,
            RequestName = nameof(GetSentHistoryQuery)
        };

        await _databaseContext.AddAsync(apiRequest, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        _loggerService.LogInformation($"Request has been logged with the system. User ID: {userId}");

        var history = await _databaseContext.SentHistory
            .AsNoTracking()
            .Include(history => history.Emails)
            .Include(history => history.Users)
            .Where(history => history.UserId == userId)
            .Select(history => new HistoryEntry
            {
                EmailFrom = history.Emails.Address,
                SentAt = history.SentAt
            })
            .ToListAsync(cancellationToken);

        var associatedUser = await _databaseContext.Users
            .AsNoTracking()
            .Where(users => users.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        var wording = history.Count == 1 ? "entry" : "entries";
        _loggerService.LogInformation($"Found {history.Count} history {wording} for requested user");

        return new GetSentHistoryQueryResult
        {
            AssociatedUser = associatedUser.UserAlias,
            HistoryEntries = history
        };
    }
}