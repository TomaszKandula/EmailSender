using MediatR;

namespace EmailSender.Backend.Application.Handlers.Queries.Users;

public class GetUserDetailsQuery : IRequest<GetUserDetailsQueryResult> { }