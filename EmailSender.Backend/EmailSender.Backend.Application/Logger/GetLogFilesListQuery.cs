using MediatR;

namespace EmailSender.Backend.Application.Handlers.Queries.Logger;

public class GetLogFilesListQuery : IRequest<GetLogFilesListQueryResult> { }