using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Shared.Resources;
using EmailSender.Persistence.Database;
using EmailSender.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

public class GetUserDetailsQueryHandler : RequestHandler<GetUserDetailsQuery, GetUserDetailsQueryResult>
{
    private readonly DatabaseContext _databaseContext;
        
    private readonly IUserService _userService;

    public GetUserDetailsQueryHandler(DatabaseContext databaseContext, IUserService userService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
    }

    public override async Task<GetUserDetailsQueryResult> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        if (userId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

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
            Status = user.Status.ToString()
        };
    }
}