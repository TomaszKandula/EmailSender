namespace EmailSenderService.Backend.SmtpService
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

        IDictionary<string, bool> IsAddressCorrect(IEnumerable<string> emailAddress);

        Task<bool> IsDomainCorrect(string emailAddress, CancellationToken cancellationToken = default);
    }
}