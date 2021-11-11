namespace EmailSender.Backend.SmtpService
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;

    public interface ISmtpClientService
    {
        EmailData EmailData { get; set; }

        ServerData ServerData { get; set; }

        Task<IEnumerable<VerifyEmail>> VerifyEmailAddress(IEnumerable<string> emails, CancellationToken cancellationToken = default);

        Task<bool> VerifyConnection(CancellationToken cancellationToken = default);

        Task<bool> Send(CancellationToken cancellationToken = default);
    }
}