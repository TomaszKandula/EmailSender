#nullable enable
namespace EmailSender.Backend.EmailService.Services.SenderService
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Database;
    using SmtpService;
    using Shared.Exceptions;
    using SmtpService.Models;

    public class SenderService : ISenderService
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ISmtpClientService _smtpClientService;
        
        public SenderService(DatabaseContext databaseContext, ISmtpClientService smtpClientService)
        {
            _databaseContext = databaseContext;
            _smtpClientService = smtpClientService;
        }

        public async Task<Guid> VerifyEmailFrom(string emailFrom, Guid? userId, CancellationToken cancellationToken)
        {
            var matchedEmailId = await _databaseContext.AllowEmail
                .AsNoTracking()
                .Include(allowEmail => allowEmail.Email)
                .Include(allowEmail => allowEmail.User)
                .Where(allowEmail => allowEmail.Email.Address == emailFrom && allowEmail.Email.IsActive)
                .Where(allowEmail => allowEmail.UserId == userId)
                .Select(allowEmail => allowEmail.EmailId)
                .FirstOrDefaultAsync(cancellationToken);

            return matchedEmailId;
        }

        public async Task Send(Configuration configuration, CancellationToken cancellationToken)
        {
            var emailData = new EmailData
            {
                From = configuration.From,
                To = configuration.To,
                Cc = configuration.Cc,
                Bcc = configuration.Bcc,
                Subject = configuration.Subject,
                HtmlBody = configuration.IsHtml ? configuration.Body : string.Empty,
                PlainText = configuration.IsHtml ? string.Empty : configuration.Body
            };

            _smtpClientService.EmailData = emailData;
            _smtpClientService.ServerData = await GetServerData(emailData.From, cancellationToken);

            var result = await _smtpClientService.Send(cancellationToken);
            if (!result.IsSucceeded)
            {
                var message = result.InnerMessage == string.Empty ? result.ErrorDesc : result.InnerMessage;
                throw new BusinessException(result.ErrorCode, message);
            }
        }

        public async Task<ErrorResult?> VerifyConnection(Guid emailId, CancellationToken cancellationToken)
        {
            _smtpClientService.ServerData = await GetServerData(emailId, cancellationToken);
            var result = await _smtpClientService.VerifyConnection(cancellationToken);

            return result.IsSucceeded ? null : new ErrorResult
            {
                ErrorCode = result.ErrorCode,
                ErrorDesc = result.ErrorDesc,
                InnerMessage = result.InnerMessage
            };
        }

        public Task<IEnumerable<VerifyEmail>> VerifyEmailAddress(IEnumerable<string> emailAddress, CancellationToken cancellationToken)
        {
            return _smtpClientService.VerifyEmailAddress(emailAddress, cancellationToken);
        }

        private async Task<ServerData> GetServerData(string address, CancellationToken cancellationToken)
        {
            var email = await _databaseContext.Email
                .AsNoTracking()
                .Where(email => email.Address == address)
                .FirstOrDefaultAsync(cancellationToken);

            if (email == null)
                return new ServerData();

            return new ServerData
            {
                Address = email.Address,
                Server = email.ServerName,
                Key = email.ServerKey,
                Port = email.ServerPort,
                IsSSL = email.ServerSsl
            };
        }

        private async Task<ServerData> GetServerData(Guid addressId, CancellationToken cancellationToken)
        {
            var email = await _databaseContext.Email
                .AsNoTracking()
                .Where(email => email.Id == addressId)
                .FirstOrDefaultAsync(cancellationToken);

            if (email == null)
                return new ServerData();

            return new ServerData
            {
                Address = email.Address,
                Server = email.ServerName,
                Key = email.ServerKey,
                Port = email.ServerPort,
                IsSSL = email.ServerSsl
            };
        }
    }
}