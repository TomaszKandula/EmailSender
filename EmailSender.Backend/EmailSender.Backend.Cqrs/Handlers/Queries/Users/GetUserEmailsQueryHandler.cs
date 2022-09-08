using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Core.Services.LoggerService;
using EmailSender.Backend.Shared.Resources;
using EmailSender.Persistence.Database;
using EmailSender.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

public class GetUserEmailsQueryHandler : RequestHandler<GetUserEmailsQuery, GetUserEmailsQueryResult>
{
    private readonly DatabaseContext _databaseContext;
        
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public GetUserEmailsQueryHandler(DatabaseContext databaseContext, IUserService userService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<GetUserEmailsQueryResult> Handle(GetUserEmailsQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var emails = await _databaseContext.UserEmails
            .AsNoTracking()
            .Include(allowEmail => allowEmail.Emails)
            .Include(allowEmail => allowEmail.Users)
            .Where(allowEmail => allowEmail.UserId == userId)
            .Select(allowEmail => allowEmail.Emails.Address)
            .ToListAsync(cancellationToken);

        var associatedUser = await _databaseContext.Users
            .AsNoTracking()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        _loggerService.LogInformation($"Found {emails.Count} email(s) for requested user");
        return new GetUserEmailsQueryResult
        {
            AssociatedUser = associatedUser.UserAlias,
            Emails = emails
        };
    }
}