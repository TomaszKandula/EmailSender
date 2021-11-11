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

        Task<ActionResult> VerifyConnection(CancellationToken cancellationToken = default);

        Task<ActionResult> Send(CancellationToken cancellationToken = default);

        Task<IEnumerable<VerifyEmail>> VerifyEmailAddress(IEnumerable<string> emails, CancellationToken cancellationToken = default);
    }
}