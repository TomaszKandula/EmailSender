namespace EmailSender.Backend.Cqrs.Handlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Requests;
    using Responses;
    using UserService;
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Core.Services.DateTimeService;

    public class GetAllowDomainsQueryHandler : TemplateHandler<GetAllowDomainsQueryRequest, GetAllowDomainsQueryResponse>
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IUserService _userService;

        private readonly IDateTimeService _dateTimeService;

        public GetAllowDomainsQueryHandler(DatabaseContext databaseContext, IUserService userService, 
            IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _userService = userService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<GetAllowDomainsQueryResponse> Handle(GetAllowDomainsQueryRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _userService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _userService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(GetAllowDomainsQueryRequest)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var hosts = await _databaseContext.AllowDomain
                .AsNoTracking()
                .Where(allowDomain => allowDomain.UserId == userId)
                .OrderBy(allowDomain => allowDomain.Host)
                .Select(allowDomain => allowDomain.Host)
                .ToListAsync(cancellationToken);

            return new GetAllowDomainsQueryResponse
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