#nullable enable
namespace EmailSender.Backend.SenderService
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using SmtpService.Models;

    public interface ISenderService
    {
        Task<Guid> VerifyEmailFrom(string emailFrom, Guid? userId, CancellationToken cancellationToken);

        Task<IEnumerable<VerifyEmail>> VerifyEmailAddress(IEnumerable<string> emailAddress, CancellationToken cancellationToken);

        Task<ErrorResult?> VerifyConnection(Guid emailId, CancellationToken cancellationToken);

        Task Send(Configuration configuration, CancellationToken cancellationToken);
    }
}