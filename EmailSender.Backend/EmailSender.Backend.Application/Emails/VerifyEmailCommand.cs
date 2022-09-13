using MediatR;

namespace EmailSender.Backend.Application.Emails;

public class VerifyEmailCommand : IRequest<VerifyEmailCommandResult>
{
    public List<string> Emails { get; set; } = new();
}