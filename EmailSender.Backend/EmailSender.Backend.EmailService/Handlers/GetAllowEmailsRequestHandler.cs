namespace EmailSender.Backend.EmailService.Handlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Requests;
    using Responses;
    using Domain.Entities;
    using Shared.Resources;
    using Shared.Exceptions;
    using Services.SenderService;
    using Shared.Services.DateTimeService;

    public class GetAllowEmailsRequestHandler : TemplateHandler<GetAllowEmailsRequest, GetAllowEmailsResponse>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly ISenderService _senderService;

        private readonly IDateTimeService _dateTimeService;

        public GetAllowEmailsRequestHandler(DatabaseContext databaseContext, ISenderService senderService, 
            IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _senderService = senderService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<GetAllowEmailsResponse> Handle(GetAllowEmailsRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(GetAllowEmailsRequest)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var emails = await _databaseContext.AllowEmail
                .AsNoTracking()
                .Include(allowEmail => allowEmail.Email)
                .Include(allowEmail => allowEmail.User)
                .Where(allowEmail => allowEmail.UserId == userId)
                .Select(allowEmail => allowEmail.Email.Address)
                .ToListAsync(cancellationToken);

            var associatedUser = await _databaseContext.User
                .AsNoTracking()
                .Where(user => user.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            return new GetAllowEmailsResponse
            {
                AssociatedUser = associatedUser.UserAlias,
                Emails = emails
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