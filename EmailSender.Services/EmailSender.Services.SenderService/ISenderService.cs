using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using EmailSender.Services.SenderService.Models;
using EmailSender.Services.SmtpService.Models;

#nullable enable
namespace EmailSender.Services.SenderService;

public interface ISenderService
{
    Task<Guid> VerifyEmailFrom(string emailFrom, Guid? userId, CancellationToken cancellationToken);

    Task<IEnumerable<VerifyEmail>> VerifyEmailAddress(IEnumerable<string> emailAddress, CancellationToken cancellationToken);

    Task VerifyConnection(Guid emailId, CancellationToken cancellationToken);

    Task Send(Configuration configuration, CancellationToken cancellationToken);
}