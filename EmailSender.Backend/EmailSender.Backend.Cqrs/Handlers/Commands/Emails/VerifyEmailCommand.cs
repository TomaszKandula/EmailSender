namespace EmailSender.Backend.Cqrs.Handlers.Commands.Emails;

using MediatR;
using System.Collections.Generic;

public class VerifyEmailCommand : IRequest<VerifyEmailCommandResult>
{
    public string PrivateKey { get; set; }

    public IEnumerable<string> Emails { get; set; }
}