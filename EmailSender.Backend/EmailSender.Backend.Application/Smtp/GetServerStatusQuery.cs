using MediatR;

namespace EmailSender.Backend.Application.Smtp;

public class GetServerStatusQuery : IRequest<Unit>
{
    public string EmailAddress { get; set; } = "";
}