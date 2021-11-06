namespace EmailSender.Backend.EmailService.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Responses;
    using Requests;
    using Domain.Entities;
    using Shared.Resources;
    using Shared.Exceptions;
    using Services.SenderService;
    using Shared.Services.DateTimeService;

    public class VerifyEmailRequestHandler : TemplateHandler<VerifyEmailRequest, VerifyEmailResponse>
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ISenderService _senderService;

        private readonly IDateTimeService _dateTimeService;

        public VerifyEmailRequestHandler(DatabaseContext databaseContext, ISenderService senderService, 
            IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _senderService = senderService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<VerifyEmailResponse> Handle(VerifyEmailRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(VerifyEmailRequest)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var result = await _senderService.VerifyEmailAddress(request.Emails, cancellationToken);

            return new VerifyEmailResponse
            {
                CheckResult = result
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