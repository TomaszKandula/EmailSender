namespace EmailSenderService.Backend.SmtpService
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using MimeKit;
    using DnsClient;
    using MimeKit.Text;
    using Shared.Resources;
    using MailKit.Net.Smtp;
    using MailKit.Security;

    public class SmtpClientService : ISmtpClientService
    {
        private readonly ISmtpClient _smtpClient;

        private readonly ILookupClient _lookupClient;

        public virtual EmailData EmailData { get; set; }

        public virtual ServerData ServerData { get; set; }

        public SecureSocketOptions SslOnConnect => ServerData.IsSSL
            ? SecureSocketOptions.SslOnConnect
            : SecureSocketOptions.None;

        public SmtpClientService(ISmtpClient smtpClient, ILookupClient lookupClient)
        {
            _smtpClient = smtpClient;
            _lookupClient = lookupClient;
        }

        public virtual async Task<ActionResult> VerifyConnection(CancellationToken cancellationToken = default)
        {
            try
            {
                await _smtpClient.ConnectAsync(ServerData.Server, ServerData.Port, SslOnConnect, cancellationToken);
                if (!_smtpClient.IsConnected)
                {
                    return new ActionResult
                    {
                        ErrorCode = nameof(ErrorCodes.SMTP_NOT_CONNECTED),
                        ErrorDesc = ErrorCodes.SMTP_NOT_CONNECTED
                    };
                }

                await _smtpClient.AuthenticateAsync(ServerData.Address, ServerData.Key, cancellationToken);
                if (!_smtpClient.IsAuthenticated)
                {
                    return new ActionResult
                    {
                        ErrorCode = nameof(ErrorCodes.SMTP_NOT_AUTHENTICATED),
                        ErrorDesc = ErrorCodes.SMTP_NOT_AUTHENTICATED
                    };
                }

                await _smtpClient.DisconnectAsync(true, cancellationToken);
                return new ActionResult { IsSucceeded = true };
            }
            catch (Exception exception)
            {
                return new ActionResult
                {
                    ErrorCode = nameof(ErrorCodes.SMTP_CLIENT_ERROR),
                    ErrorDesc = ErrorCodes.SMTP_CLIENT_ERROR,
                    InnerMessage = exception.Message
                };
            }
        }

        public virtual async Task<ActionResult> Send(CancellationToken cancellationToken = default)
        {
            try
            {
                var newMail = new MimeMessage();

                newMail.From.Add(MailboxAddress.Parse(EmailData.From));
                newMail.Subject = EmailData.Subject;

                foreach (var item in EmailData.To) 
                    newMail.To.Add(MailboxAddress.Parse(item));

                if (EmailData.Cc != null && !EmailData.Cc.Any())
                    foreach (var item in EmailData.Cc) newMail.Cc.Add(MailboxAddress.Parse(item));

                if (EmailData.Bcc != null && !EmailData.Bcc.Any())
                    foreach (var item in EmailData.Bcc) newMail.Bcc.Add(MailboxAddress.Parse(item));

                if (!string.IsNullOrEmpty(EmailData.PlainText)) 
                    newMail.Body = new TextPart(TextFormat.Plain) { Text = EmailData.PlainText };

                if (!string.IsNullOrEmpty(EmailData.HtmlBody)) 
                    newMail.Body = new TextPart(TextFormat.Html) { Text = EmailData.HtmlBody };

                await _smtpClient.ConnectAsync(ServerData.Server, ServerData.Port, SslOnConnect, cancellationToken);
                await _smtpClient.AuthenticateAsync(ServerData.Address, ServerData.Key, cancellationToken);

                await _smtpClient.SendAsync(newMail, cancellationToken);
                await _smtpClient.DisconnectAsync(true, cancellationToken);

                return new ActionResult { IsSucceeded = true };
            } 
            catch (Exception exception)
            {
                return new ActionResult
                {
                    ErrorCode = nameof(ErrorCodes.SMTP_CLIENT_ERROR),
                    ErrorDesc = ErrorCodes.SMTP_CLIENT_ERROR,
                    InnerMessage = exception.Message
                };
            }
        }

        public virtual IDictionary<string, bool> IsAddressCorrect(IEnumerable<string> emailAddress)
        {
            var results = new Dictionary<string, bool>();

            foreach (var item in emailAddress)
            {
                try
                {
                    var mailAddress = new MailAddress(item);
                    results.Add(mailAddress.Address, true);
                }
                catch (FormatException)
                {
                    results.Add(item, false);
                }
            }

            return results;
        }

        public virtual async Task<bool> IsDomainCorrect(string emailAddress, CancellationToken cancellationToken = default)
        {
            try
            {
                var getEmailDomain = emailAddress.Split("@");
                var emailDomain = getEmailDomain[1];

                var checkRecordA = await _lookupClient.QueryAsync(emailDomain, QueryType.A, QueryClass.IN, cancellationToken);
                var checkRecordAaaa = await _lookupClient.QueryAsync(emailDomain, QueryType.AAAA, QueryClass.IN, cancellationToken);
                var checkRecordMx = await _lookupClient.QueryAsync(emailDomain, QueryType.MX, QueryClass.IN, cancellationToken);

                var recordA = checkRecordA.Answers.Where(record => record.RecordType == DnsClient.Protocol.ResourceRecordType.A);
                var recordAaaa = checkRecordAaaa.Answers.Where(record => record.RecordType == DnsClient.Protocol.ResourceRecordType.AAAA);
                var recordMx = checkRecordMx.Answers.Where(record => record.RecordType == DnsClient.Protocol.ResourceRecordType.MX);

                var isRecordA = recordA.Any();
                var isRecordAaaa = recordAaaa.Any();
                var isRecordMx = recordMx.Any();

                return isRecordA || isRecordAaaa || isRecordMx;
            }
            catch (DnsResponseException)
            {
                return false;
            }
        }
    }
}