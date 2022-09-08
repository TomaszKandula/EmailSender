using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using EmailSender.Services.SmtpService.Models;

namespace EmailSender.Services.SmtpService;

public interface ISmtpClientService
{
    EmailData EmailData { get; set; }

    ServerData ServerData { get; set; }

    Task<IEnumerable<VerifyEmail>> VerifyEmailAddress(IEnumerable<string> emails, CancellationToken cancellationToken = default);

    Task VerifyConnection(CancellationToken cancellationToken = default);

    Task Send(CancellationToken cancellationToken = default);
}