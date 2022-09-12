using MediatR;

namespace EmailSender.Backend.Application.Users.Queries;

public class GetUserDetailsQuery : IRequest<GetUserDetailsQueryResult> { }