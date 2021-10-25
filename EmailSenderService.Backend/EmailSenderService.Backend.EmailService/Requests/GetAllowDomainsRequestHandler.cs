namespace EmailSenderService.Backend.EmailService.Requests
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

    public class GetAllowDomainsRequestHandler : TemplateHandler<GetAllowDomainsRequest, GetAllowDomainsResponse>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly ISenderService _senderService;

        public GetAllowDomainsRequestHandler(DatabaseContext databaseContext, ISenderService senderService)
        {
            _databaseContext = databaseContext;
            _senderService = senderService;
        }

        public override async Task<GetAllowDomainsResponse> Handle(GetAllowDomainsRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var hosts = await _databaseContext.AllowDomain
                .AsNoTracking()
                .Where(allowDomain => allowDomain.UserId == userId)
                .OrderBy(allowDomain => allowDomain.Host)
                .Select(allowDomain => allowDomain.Host)
                .ToListAsync(cancellationToken);

            return new GetAllowDomainsResponse
            {
                Hosts = hosts
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