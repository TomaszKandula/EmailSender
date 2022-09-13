using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Shared.Resources;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Backend.Application.Logger;

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