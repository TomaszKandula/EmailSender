#nullable enable
namespace EmailSender.Backend.SenderService
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
    using Core.Exceptions;
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

        /// <summary>
        /// Checks if given email address is registered for sending an email. Returns null for unknown email address.
        /// </summary>
        /// <param name="emailFrom">Email address registered for sending.</param>
        /// <param name="userId">User ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Email ID (Guid).</returns>
        public async Task<Guid> VerifyEmailFrom(string emailFrom, Guid? userId, CancellationToken cancellationToken)
        {
            var matchedEmailId = await _databaseContext.UserEmails
                .AsNoTracking()
                .Include(allowEmail => allowEmail.Emails)
                .Include(allowEmail => allowEmail.Users)
                .Where(allowEmail => allowEmail.Emails.Address == emailFrom && allowEmail.Emails.IsActive)
                .Where(allowEmail => allowEmail.UserId == userId)
                .Select(allowEmail => allowEmail.EmailId)
                .FirstOrDefaultAsync(cancellationToken);

            return matchedEmailId;
        }

        /// <summary>
        /// Checks list of email addresses.
        /// </summary>
        /// <param name="emailAddress">List of email addresses.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of verified emails.</returns>
        public async Task<IEnumerable<VerifyEmail>> VerifyEmailAddress(IEnumerable<string> emailAddress, CancellationToken cancellationToken)
        {
            return await _smtpClientService.VerifyEmailAddress(emailAddress, cancellationToken);
        }

        /// <summary>
        /// Checks connection to SMTP server for given email address.
        /// Email address must be registered within the system.
        /// </summary>
        /// <param name="emailId">ID of registered email address.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Error object or null.</returns>
        public async Task VerifyConnection(Guid emailId, CancellationToken cancellationToken)
        {
            _smtpClientService.ServerData = await GetServerData(emailId, cancellationToken);
            await _smtpClientService.VerifyConnection(cancellationToken);
        }

        /// <summary>
        /// Sends an email using data passed in configuration object.
        /// </summary>
        /// <param name="configuration">
        /// Holds email details including fields FROM, TO, CC, BCC, SUBJECT.
        /// Email message can be either plain text or HTML formatted. 
        /// </param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <exception cref="BusinessException">Throws HTTP status code 400.</exception>
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

            await _smtpClientService.Send(cancellationToken);
        }

        /// <summary>
        /// Returns server data for given email address that allows to connect and send an email from SMTP server.
        /// </summary>
        /// <param name="address">Email address.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Object containing server details.</returns>
        private async Task<ServerData> GetServerData(string address, CancellationToken cancellationToken)
        {
            var email = await _databaseContext.Emails
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

        /// <summary>
        /// Returns server data for given email address that allows to connect and send an email from SMTP server.
        /// </summary>
        /// <param name="addressId">ID of registered email address.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Object containing server details.</returns>
        private async Task<ServerData> GetServerData(Guid addressId, CancellationToken cancellationToken)
        {
            var email = await _databaseContext.Emails
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