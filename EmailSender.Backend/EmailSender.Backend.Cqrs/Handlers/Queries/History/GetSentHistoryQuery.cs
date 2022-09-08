using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.History;

public class GetSentHistoryQuery : IRequest<GetSentHistoryQueryResult> { }