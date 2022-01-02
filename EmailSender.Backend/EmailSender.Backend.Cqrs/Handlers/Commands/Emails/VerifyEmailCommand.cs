namespace EmailSender.Backend.Cqrs.Handlers.Commands.Emails;

using MediatR;
using System.Collections.Generic;

public class VerifyEmailCommand : IRequest<VerifyEmailCommandResult>
{
    public IEnumerable<string> Emails { get; set; }
}