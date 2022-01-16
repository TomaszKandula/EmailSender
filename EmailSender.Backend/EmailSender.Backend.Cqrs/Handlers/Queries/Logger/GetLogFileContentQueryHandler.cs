namespace EmailSender.Backend.Cqrs.Handlers.Queries.Logger;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Exceptions;
using Shared.Resources;

public class GetLogFileContentQueryHandler : RequestHandler<GetLogFileContentQuery, FileContentResult>
{
    public override async Task<FileContentResult> Handle(GetLogFileContentQuery request, CancellationToken cancellationToken)
    {
        var pathToFolder = $"{AppDomain.CurrentDomain.BaseDirectory}logs";
        if (!Directory.Exists(pathToFolder))
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        var fullFilePath = $"{pathToFolder}{Path.DirectorySeparatorChar}{request.LogFileName}";
        if (!File.Exists(fullFilePath))
            throw new BusinessException(nameof(ErrorCodes.FILE_NOT_FOUND), ErrorCodes.FILE_NOT_FOUND);

        var fileContent = await File.ReadAllBytesAsync(fullFilePath, cancellationToken);
        return new FileContentResult(fileContent, "text/plain");
    }
}