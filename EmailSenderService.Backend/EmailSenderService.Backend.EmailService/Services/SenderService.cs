namespace EmailSenderService.Backend.EmailService.Services
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Database;
    using SmtpService;
    using Domain.Entities;
    using Shared.Exceptions;
    using Shared.Services.LoggerService;

    public class SenderService : ISenderService
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ISmtpClientService _smtpClientService;

        private readonly ILoggerService _loggerService;
        
        public SenderService(DatabaseContext databaseContext, ISmtpClientService smtpClientService, ILoggerService loggerService)
        {
            _databaseContext = databaseContext;
            _smtpClientService = smtpClientService;
            _loggerService = loggerService;
        }

        public async Task<bool> IsDomainAllowed(string domainName, CancellationToken cancellationToken)
        {
            var domains = await _databaseContext.AllowDomain
                .Where(allowDomain => allowDomain.Host == domainName)
                .ToListAsync(cancellationToken);

            var isDomainAllowed = domains.Any();
            if (!isDomainAllowed) 
                _loggerService.LogWarning($"Domain '{domainName}' is not registered within the system.");

            return isDomainAllowed;
        }

        public async Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken)
        {
            var keys = await _databaseContext.User
                .Where(user => user.PrivateKey == privateKey)
                .ToListAsync(cancellationToken);

            var isPrivateKeyExists = keys.Any();
            if (!isPrivateKeyExists)
                _loggerService.LogWarning($"Key '{privateKey}' is not registered within the system.");
            
            return isPrivateKeyExists;
        }

        public async Task<User> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken)
        {
            return await _databaseContext.User
                .Where(user => user.PrivateKey == privateKey)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Guid> VerifyEmailFrom(string emailFrom, Guid userId, CancellationToken cancellationToken)
        {
            var matchedEmailId = await _databaseContext.AllowEmail
                .Include(allowEmail => allowEmail.Email)
                .Include(allowEmail => allowEmail.User)
                .Where(allowEmail => allowEmail.Email.Address == emailFrom && allowEmail.Email.IsActive == true)
                .Where(allowEmail => allowEmail.UserId == userId)
                .Select(allowEmail => allowEmail.EmailId)
                .FirstOrDefaultAsync(cancellationToken);

            return matchedEmailId;
        }

        public async Task Send(Configuration configuration, CancellationToken cancellationToken)
        {
            _smtpClientService.From = configuration.From;
            _smtpClientService.Tos = configuration.To;
            _smtpClientService.Ccs = configuration.Cc;
            _smtpClientService.Bccs = configuration.Bcc;
            _smtpClientService.Subject = configuration.Subject;

            if (configuration.IsHtml)
            {
                _smtpClientService.HtmlBody = configuration.Body;
            }
            else
            {
                _smtpClientService.PlainText = configuration.Body;
            }

            var result = await _smtpClientService.Send(cancellationToken);
            if (!result.IsSucceeded)
            {
                var message = result.InnerMessage == string.Empty ? result.ErrorDesc : result.InnerMessage;
                throw new BusinessException(result.ErrorCode, message);
            }
        }
    }
}