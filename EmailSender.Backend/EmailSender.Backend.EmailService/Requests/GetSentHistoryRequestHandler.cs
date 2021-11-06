namespace EmailSender.Backend.EmailService.Requests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Database;
    using Responses;
    using Domain.Entities;
    using Shared.Resources;
    using Shared.Exceptions;
    using Services.SenderService;
    using Shared.Services.DateTimeService;

    public class GetSentHistoryRequestHandler : TemplateHandler<GetSentHistoryRequest, GetSentHistoryResponse>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly ISenderService _senderService;

        private readonly IDateTimeService _dateTimeService;

        public GetSentHistoryRequestHandler(DatabaseContext databaseContext, ISenderService senderService, 
            IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _senderService = senderService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<GetSentHistoryResponse> Handle(GetSentHistoryRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(GetSentHistoryRequest)
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