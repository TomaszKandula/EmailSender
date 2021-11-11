namespace EmailSender.Backend.Cqrs.Handlers
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Requests;
    using UserService;
    using SenderService;
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Core.Services.DateTimeService;

    public class GetServerStatusHandler : TemplateHandler<GetServerStatusRequest, Unit>
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IUserService _userService;
        
        private readonly ISenderService _senderService;

        private readonly IDateTimeService _dateTimeService;

        public GetServerStatusHandler(DatabaseContext databaseContext, IUserService userService, 
            ISenderService senderService, IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _userService = userService;
            _senderService = senderService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<Unit> Handle(GetServerStatusRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _userService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _userService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);
            var emailId = await _senderService.VerifyEmailFrom(request.EmailAddress, userId, cancellationToken);

            VerifyArguments(isKeyValid, userId, emailId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(GetServerStatusRequest)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var result = await _senderService.VerifyConnection(emailId, cancellationToken);
            return result == null 
                ? Unit.Value 
                : throw new ServerException(nameof(ErrorCodes.SMTP_FAILED),$"SMTP service verification failed. Reason: {result.ErrorDesc}");
        }

        private static void VerifyArguments(bool isKeyValid, Guid? userId, Guid? emailId)
        {
            if (!isKeyValid)
            {
                var message = $"Cannot verify SMTP service. Reason: {ErrorCodes.INVALID_PRIVATE_KEY}";
                throw new ServerException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), message);
            }

            if (userId == null || userId == Guid.Empty)
            {
                var message = $"Cannot verify SMTP service. Reason: {ErrorCodes.INVALID_ASSOCIATED_USER}";
                throw new ServerException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), message);
            }

            if (emailId == null || emailId == Guid.Empty)
            {
                var message = $"Cannot verify SMTP service. Reason: {ErrorCodes.INVALID_ASSOCIATED_EMAIL}";
                throw new ServerException(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL), message);
            }
        }
    }
}