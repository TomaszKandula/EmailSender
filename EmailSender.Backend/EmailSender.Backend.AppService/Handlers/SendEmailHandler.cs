namespace EmailSender.Backend.AppService.Handlers
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Requests;
    using UserService;
    using SenderService;
    using Domain.Entities;
    using Shared.Resources;
    using Shared.Exceptions;
    using SenderService.Models;
    using Shared.Services.DateTimeService;

    public class SendEmailHandler : TemplateHandler<SendEmailRequest, Unit>
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IUserService _userService;

        private readonly ISenderService _senderService;

        private readonly IDateTimeService _dateTimeService;

        public SendEmailHandler(DatabaseContext databaseContext, IUserService userService,
            ISenderService senderService, IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _userService = userService;
            _senderService = senderService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<Unit> Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _userService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _userService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);
            var emailId = await _senderService.VerifyEmailFrom(request.From, userId, cancellationToken);

            VerifyArguments(isKeyValid, userId, emailId);

            var apiRequest = new RequestHistory
            {
                UserId = userId,
                Requested = _dateTimeService.Now,
                RequestName = nameof(SendEmailRequest)
            };

            await _databaseContext.AddAsync(apiRequest, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var configuration = new Configuration
            {
                From = request.From,
                Subject = request.Subject,
                To = request.To,
                Cc = request.Cc,
                Bcc = request.Bcc,
                Body = request.Body,
                IsHtml = request.IsHtml
            };

            await _senderService.Send(configuration, cancellationToken);

            var history = new EmailHistory
            {
                UserId = userId,
                EmailId = emailId,
                Sent = _dateTimeService.Now
            };

            await _databaseContext.EmailHistory.AddAsync(history, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }

        private static void VerifyArguments(bool isKeyValid, Guid? userId, Guid? emailId)
        {
            if (!isKeyValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);

            if (userId == null || userId == Guid.Empty)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

            if (emailId == null || emailId == Guid.Empty)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL), ErrorCodes.INVALID_ASSOCIATED_EMAIL);
        }
    }
}