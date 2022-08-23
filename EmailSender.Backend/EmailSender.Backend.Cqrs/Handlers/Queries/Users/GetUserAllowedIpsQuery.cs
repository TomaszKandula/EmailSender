namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

using MediatR;

public class GetUserAllowedIpsQuery : IRequest<GetUserAllowedIpsQueryResult> { }