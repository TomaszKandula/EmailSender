using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

public class GetUserEmailsQuery : IRequest<GetUserEmailsQueryResult> { }