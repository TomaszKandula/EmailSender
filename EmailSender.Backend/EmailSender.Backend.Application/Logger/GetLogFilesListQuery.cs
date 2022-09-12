using MediatR;

namespace EmailSender.Backend.Application.Logger;

public class GetLogFilesListQuery : IRequest<GetLogFilesListQueryResult> { }