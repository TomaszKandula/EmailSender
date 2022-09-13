using EmailSender.Services.SmtpService.Models;

namespace EmailSender.Services.SmtpService;

public interface ISmtpClientService
{
    EmailData EmailData { get; set; }

    ServerData ServerData { get; set; }

    Task<List<VerifyEmail>> VerifyEmailAddress(IEnumerable<string> emails, CancellationToken cancellationToken = default);

    Task VerifyConnection(CancellationToken cancellationToken = default);

    Task Send(CancellationToken cancellationToken = default);
}