using MediatR;

namespace EmailSender.Backend.Application.Handlers.Queries.History;

public class GetSentHistoryQuery : IRequest<GetSentHistoryQueryResult> { }