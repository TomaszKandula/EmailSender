namespace EmailSender.Backend.EmailService.Requests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Services;
    using Database;
    using Responses;
    using Shared.Resources;
    using Shared.Exceptions;

    public class GetUserDetailsRequestHandler : TemplateHandler<GetUserDetailsRequest, GetUserDetailsResponse>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly ISenderService _senderService;

        public GetUserDetailsRequestHandler(DatabaseContext databaseContext, ISenderService senderService)
        {
            _databaseContext = databaseContext;
            _senderService = senderService;
        }

        public override async Task<GetUserDetailsResponse> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var user = await _databaseContext.User
                .Where(user => user.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            return new GetUserDetailsResponse
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

        private static void VerifyArguments(bool isKeyValid, Guid? userId)
        {
            if (!isKeyValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);

            if (userId == null || userId == Guid.Empty)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);
        }
    }
}