namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

using MediatR;

public class GetUserEmailsQuery : IRequest<GetUserEmailsQueryResult> { }