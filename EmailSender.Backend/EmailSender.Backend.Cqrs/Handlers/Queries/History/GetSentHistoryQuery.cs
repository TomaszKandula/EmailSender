namespace EmailSender.Backend.Cqrs.Handlers.Queries.History;

using MediatR;

public class GetSentHistoryQuery : IRequest<GetSentHistoryQueryResult> { }