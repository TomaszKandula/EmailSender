using MediatR;

namespace EmailSender.Backend.Application.Handlers.Queries.Users;

public class GetUserAllowedIpsQuery : IRequest<GetUserAllowedIpsQueryResult> { }