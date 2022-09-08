using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.Logger;

public class GetLogFileContentQuery : IRequest<FileContentResult>
{
    public string LogFileName { get; set; }
}