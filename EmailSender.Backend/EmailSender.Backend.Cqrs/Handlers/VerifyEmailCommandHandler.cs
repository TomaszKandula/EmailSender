namespace EmailSender.Backend.Cqrs.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Requests;
    using Responses;
    using UserService;
    using SenderService;
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Core.Services.DateTimeService;

    public class VerifyEmailCommandHandler : RequestHandler<VerifyEmailCommandRequest, VerifyEmailCommandResponse>
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IUserService _userService;

        private readonly ISenderService _senderService;

        private readonly IDateTimeService _dateTimeService;

        public VerifyEmailCommandHandler(DatabaseContext databaseContext, IUserService userService,
            ISenderService senderService, IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _userService = userService;
            _senderService = senderService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<VerifyEmailCommandResponse> Handle(VerifyEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _userService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _userService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(VerifyEmailCommandRequest)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var result = await _senderService.VerifyEmailAddress(request.Emails, cancellationToken);

            return new VerifyEmailCommandResponse
            {
                CheckResult = result
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