namespace EmailSender.Backend.Cqrs.Handlers.Queries.Smtp;

using MediatR;

public class GetServerStatusQuery : IRequest<Unit>
{
    public string EmailAddress { get; set; }
}