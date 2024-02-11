using MediatR;

namespace EmailSender.Backend.Application.Mailcow;

public class GetMailcowStatusQuery : IRequest<GetMailcowStatusQueryResult> { }
