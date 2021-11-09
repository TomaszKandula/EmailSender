namespace EmailSender.Backend.EmailService.Handlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Requests;
    using Responses;
    using Database;
    using Domain.Entities;
    using Shared.Resources;
    using Shared.Exceptions;
    using Services.UserService;
    using Services.BillingService;
    using Shared.Services.DateTimeService;

    public class GetUserBillingHandler : TemplateHandler<GetUserBillingRequest, GetUserBillingResponse>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly IBillingService _billingService;

        private readonly IUserService _userService;

        private readonly IDateTimeService _dateTimeService;

        public GetUserBillingHandler(DatabaseContext databaseContext, IBillingService billingService, 
            IUserService userService, IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _billingService = billingService;
            _userService = userService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<GetUserBillingResponse> Handle(GetUserBillingRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _userService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _userService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(GetUserBillingRequest)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var billing = await _billingService.GetUserBilling(request.BillingId, cancellationToken);
            var userAlias = await _databaseContext.User
                .AsNoTracking()
                .Where(user => user.Id == userId)
                .Select(user => user.UserAlias)
                .FirstOrDefaultAsync(cancellationToken);

            return new GetUserBillingResponse
            {
                UserAlias = userAlias,
                Amount = billing.Amount,
                CurrencyIso = billing.CurrencyIso,
                ValueDate = billing.ValueDate,
                DueDate = billing.DueDate,
                InvoiceSentDate = billing.InvoiceSentDate
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