namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using UserService;
    using Shared.Models;
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Core.Services.DateTimeService;

    public class GetSentHistoryQueryHandler : RequestHandler<GetSentHistoryQuery, GetSentHistoryQueryResult>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly IUserService _userService;

        private readonly IDateTimeService _dateTimeService;

        public GetSentHistoryQueryHandler(DatabaseContext databaseContext, IUserService userService, 
            IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _userService = userService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<GetSentHistoryQueryResult> Handle(GetSentHistoryQuery request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _userService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _userService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(GetSentHistoryQuery)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var history = await _databaseContext.EmailHistory
                .AsNoTracking()
                .Include(history => history.Email)
                .Include(history => history.User)
                .Where(history => history.UserId == userId)
                .Select(history => new HistoryEntry
                {
                    EmailFrom = history.Email.Address,
                    SentAt = history.Sent
                })
                .ToListAsync(cancellationToken);

            var associatedUser = await _databaseContext.User
                .AsNoTracking()
                .Where(user => user.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            return new GetSentHistoryQueryResult
            {
                AssociatedUser = associatedUser.UserAlias,
                HistoryEntries = history
            };
        }

        private static void VerifyArguments(bool isKeyValid, Guid? userId)
        {
            if (!isKeyValid)
                throw new AccessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);

            if (userId == null || userId == Guid.Empty)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);
        }
    }
}