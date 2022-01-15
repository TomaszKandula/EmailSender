namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Domain.Entities;
using Shared.Resources;
using Services.UserService;
using Core.Services.LoggerService;
using Core.Services.DateTimeService;

public class GetUserDetailsQueryHandler : RequestHandler<GetUserDetailsQuery, GetUserDetailsQueryResult>
{
    private readonly DatabaseContext _databaseContext;
        
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly ILoggerService _loggerService;

    public GetUserDetailsQueryHandler(DatabaseContext databaseContext, IUserService userService, 
        IDateTimeService dateTimeService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
    }

    public override async Task<GetUserDetailsQueryResult> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);

        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var apiRequest = new RequestsHistory
        {
            UserId = userId,
            Requested = _dateTimeService.Now,
            RequestName = nameof(GetUserDetailsQuery)
        };

        await _databaseContext.AddAsync(apiRequest, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        _loggerService.LogInformation($"Request has been logged with the system. User ID: {userId}");

        var user = await _databaseContext.Users
            .AsNoTracking()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        return new GetUserDetailsQueryResult
        {
            UserAlias = user.UserAlias,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
            Registered = user.Registered,
            Status = user.IsActivated 
                ? "User account is active" 
                : "User account is inactive"
        };
    }
}