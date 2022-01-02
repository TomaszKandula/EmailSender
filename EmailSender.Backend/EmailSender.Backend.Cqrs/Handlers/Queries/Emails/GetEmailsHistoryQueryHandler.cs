namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails;

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
using Core.Services.DateTimeService;

public class GetEmailsHistoryQueryHandler : RequestHandler<GetEmailsHistoryQuery, GetEmailsHistoryQueryResult>
{
    private readonly DatabaseContext _databaseContext;
        
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    public GetEmailsHistoryQueryHandler(DatabaseContext databaseContext, IUserService userService, 
        IDateTimeService dateTimeService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<GetEmailsHistoryQueryResult> Handle(GetEmailsHistoryQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);

        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var apiRequest = new RequestsHistory
        {
            UserId = userId,
            Requested = _dateTimeService.Now,
            RequestName = nameof(GetEmailsHistoryQuery)
        };

        await _databaseContext.AddAsync(apiRequest, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        var history = await _databaseContext.EmailsHistory
            .AsNoTracking()
            .Include(history => history.Emails)
            .Include(history => history.Users)
            .Where(history => history.UserId == userId)
            .Select(history => new HistoryEntry
            {
                EmailFrom = history.Emails.Address,
                SentAt = history.Sent
            })
            .ToListAsync(cancellationToken);

        var associatedUser = await _databaseContext.Users
            .AsNoTracking()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        return new GetEmailsHistoryQueryResult
        {
            AssociatedUser = associatedUser.UserAlias,
            HistoryEntries = history
        };
    }
}