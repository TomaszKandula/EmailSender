using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Backend.Shared.Models;
using EmailSender.Backend.Shared.Resources;
using EmailSender.Persistence.Database;
using EmailSender.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Backend.Application.History;

public class GetRequestHistoryQueryHandler : RequestHandler<GetRequestHistoryQuery, GetRequestHistoryQueryResult>
{
    private readonly DatabaseContext _databaseContext;

    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public GetRequestHistoryQueryHandler(DatabaseContext databaseContext, IUserService userService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<GetRequestHistoryQueryResult> Handle(GetRequestHistoryQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var history = await _databaseContext.RequestsHistory
            .AsNoTracking()
            .Where(history => history.UserId == userId)
            .Select(history => new RequestHistoryEntry
            {
                Action = history.RequestName,
                RequestedAt = history.RequestedAt
            })
            .ToListAsync(cancellationToken);

        var associatedUser = await _databaseContext.Users
            .AsNoTracking()
            .Where(users => users.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        var wording = history.Count == 1 ? "entry" : "entries";
        _loggerService.LogInformation($"Found {history.Count} history {wording} for requested user");

        return new GetRequestHistoryQueryResult
        {
            AssociatedUser = associatedUser.UserAlias,
            HistoryEntries = history
        };
    }
}