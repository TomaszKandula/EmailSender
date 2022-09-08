using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.Smtp;

public class GetServerStatusQuery : IRequest<Unit>
{
    public string EmailAddress { get; set; }
}