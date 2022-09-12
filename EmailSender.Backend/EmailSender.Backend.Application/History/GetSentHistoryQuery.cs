using MediatR;

namespace EmailSender.Backend.Application.History;

public class GetSentHistoryQuery : IRequest<GetSentHistoryQueryResult> { }