namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

using MediatR;

public class GetUserDetailsQuery : IRequest<GetUserDetailsQueryResult> { }