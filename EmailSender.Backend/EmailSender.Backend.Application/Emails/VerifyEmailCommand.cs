using MediatR;
using System.Collections.Generic;

namespace EmailSender.Backend.Application.Emails;

public class VerifyEmailCommand : IRequest<VerifyEmailCommandResult>
{
    public IEnumerable<string> Emails { get; set; }
}