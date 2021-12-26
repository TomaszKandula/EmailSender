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
    using BillingService;
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Core.Services.DateTimeService;

    public class GetAllUserBillingsQueryHandler : RequestHandler<GetAllUserBillingsQueryRequest, GetAllUserBillingsQueryResponse>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly IBillingService _billingService;

        private readonly IUserService _userService;

        private readonly IDateTimeService _dateTimeService;

        public GetAllUserBillingsQueryHandler(DatabaseContext databaseContext, IBillingService billingService, 
            IUserService userService, IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _billingService = billingService;
            _userService = userService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<GetAllUserBillingsQueryResponse> Handle(GetAllUserBillingsQueryRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _userService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _userService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(GetAllUserBillingsQueryRequest)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var billings = await _billingService.GetAllUserBillings(userId, cancellationToken);
            var userAlias = await _databaseContext.User
                .AsNoTracking()
                .Where(user => user.Id == userId)
                .Select(user => user.UserAlias)
                .FirstOrDefaultAsync(cancellationToken);

            return new GetAllUserBillingsQueryResponse
            {
                UserAlias = userAlias,
                Billings = billings
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