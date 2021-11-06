namespace EmailSender.Backend.EmailService.Requests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Responses;
    using Shared.Resources;
    using Shared.Exceptions;
    using Services.SenderService;

    public class GetAllowEmailsRequestHandler : TemplateHandler<GetAllowEmailsRequest, GetAllowEmailsResponse>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly ISenderService _senderService;

        public GetAllowEmailsRequestHandler(DatabaseContext databaseContext, ISenderService senderService)
        {
            _databaseContext = databaseContext;
            _senderService = senderService;
        }

        public override async Task<GetAllowEmailsResponse> Handle(GetAllowEmailsRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

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