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

public class GetUserAllowedIpsQueryHandler : RequestHandler<GetUserAllowedIpsQuery, GetUserAllowedIpsQueryResult>
{
    private readonly DatabaseContext _databaseContext;

    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly ILoggerService _loggerService;

    public GetUserAllowedIpsQueryHandler(DatabaseContext databaseContext, IUserService userService, 
        IDateTimeService dateTimeService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
    }

    public override async Task<GetUserAllowedIpsQueryResult> Handle(GetUserAllowedIpsQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

        var apiRequest = new RequestsHistory
        {
            UserId = userId,
            Requested = _dateTimeService.Now,
            RequestName = nameof(GetUserAllowedIpsQuery)
        };

        await _databaseContext.AddAsync(apiRequest, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        _loggerService.LogInformation($"Request has been logged with the system. User ID: {userId}");

        var ipList = await _databaseContext.UserAllowedIps
            .AsNoTracking()
            .Where(ips => ips.UserId == userId)
            .OrderBy(ips => ips.IpAddress)
            .Select(ips => ips.IpAddress)
            .ToListAsync(cancellationToken);

        _loggerService.LogInformation($"Found {ipList.Count} address(es) for requested user");
        return new GetUserAllowedIpsQueryResult
        {
            IpList = ipList
        };
    }
}