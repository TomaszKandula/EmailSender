using MediatR;
using System.Collections.Generic;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Emails;

public class VerifyEmailCommand : IRequest<VerifyEmailCommandResult>
{
    public IEnumerable<string> Emails { get; set; }
}