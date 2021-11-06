namespace EmailSender.Backend.EmailService.Requests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Responses;
    using Domain.Entities;
    using Shared.Resources;
    using Shared.Exceptions;
    using Services.SenderService;
    using Shared.Services.DateTimeService;

    public class GetAllowDomainsRequestHandler : TemplateHandler<GetAllowDomainsRequest, GetAllowDomainsResponse>
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ISenderService _senderService;

        private readonly IDateTimeService _dateTimeService;

        public GetAllowDomainsRequestHandler(DatabaseContext databaseContext, ISenderService senderService, 
            IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _senderService = senderService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<GetAllowDomainsResponse> Handle(GetAllowDomainsRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(GetAllowDomainsRequest)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

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