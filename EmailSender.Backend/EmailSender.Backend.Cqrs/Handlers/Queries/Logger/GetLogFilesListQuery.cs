using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.Logger;

public class GetLogFilesListQuery : IRequest<GetLogFilesListQueryResult> { }