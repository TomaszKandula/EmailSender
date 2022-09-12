using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Backend.Shared.Resources;
using EmailSender.Persistence.Database;
using EmailSender.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Backend.Application.Users.Queries;

public class GetUserAllowedIpsQueryHandler : RequestHandler<GetUserAllowedIpsQuery, GetUserAllowedIpsQueryResult>
{
    private readonly DatabaseContext _databaseContext;

    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public GetUserAllowedIpsQueryHandler(DatabaseContext databaseContext, IUserService userService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<GetUserAllowedIpsQueryResult> Handle(GetUserAllowedIpsQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

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