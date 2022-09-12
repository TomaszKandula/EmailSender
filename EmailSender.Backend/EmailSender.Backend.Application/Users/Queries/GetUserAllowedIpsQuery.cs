using MediatR;

namespace EmailSender.Backend.Application.Users.Queries;

public class GetUserAllowedIpsQuery : IRequest<GetUserAllowedIpsQueryResult> { }