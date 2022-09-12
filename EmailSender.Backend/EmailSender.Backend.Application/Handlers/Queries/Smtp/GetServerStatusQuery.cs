using MediatR;

namespace EmailSender.Backend.Application.Handlers.Queries.Smtp;

public class GetServerStatusQuery : IRequest<Unit>
{
    public string EmailAddress { get; set; }
}