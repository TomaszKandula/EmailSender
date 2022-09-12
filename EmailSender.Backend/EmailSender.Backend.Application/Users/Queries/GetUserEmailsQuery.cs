using MediatR;

namespace EmailSender.Backend.Application.Users.Queries;

public class GetUserEmailsQuery : IRequest<GetUserEmailsQueryResult> { }