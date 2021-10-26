namespace EmailSenderService.Backend.EmailService.Requests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Services;
    using Database;
    using Responses;
    using Shared.Resources;
    using Shared.Exceptions;

    public class GetSentHistoryRequestHandler : TemplateHandler<GetSentHistoryRequest, GetSentHistoryResponse>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly ISenderService _senderService;

        public GetSentHistoryRequestHandler(DatabaseContext databaseContext, ISenderService senderService)
        {
            _databaseContext = databaseContext;
            _senderService = senderService;
        }

        public override async Task<GetSentHistoryResponse> Handle(GetSentHistoryRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var history = await _databaseContext.History
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

            return new GetSentHistoryResponse
            {
                AssociatedUser = associatedUser.UserAlias,
                HistoryEntries = history
            };
        }

        private static void VerifyArguments(bool isKeyValid, Guid? userId)
        {
            if (!isKeyValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);

            if (userId == null || userId == Guid.Empty)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);
        }
    }
}