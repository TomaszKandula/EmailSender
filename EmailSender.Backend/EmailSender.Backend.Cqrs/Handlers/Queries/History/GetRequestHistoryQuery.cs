using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.History;

public class GetRequestHistoryQuery : IRequest<GetRequestHistoryQueryResult> { }
