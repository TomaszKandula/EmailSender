namespace EmailSender.Backend.Application.Logger;

public class GetLogFilesListQueryHandler : RequestHandler<GetLogFilesListQuery, GetLogFilesListQueryResult>
{
    public override async Task<GetLogFilesListQueryResult> Handle(GetLogFilesListQuery request, CancellationToken cancellationToken)
    {
        var pathToFolder = $"{AppDomain.CurrentDomain.BaseDirectory}logs";
        if (!Directory.Exists(pathToFolder))
            return new GetLogFilesListQueryResult();

        var fullPathFileList = Directory.EnumerateFiles(pathToFolder, "*.txt", SearchOption.TopDirectoryOnly);
        var logFiles = new List<string>();

        foreach (var item in fullPathFileList)
        {
            logFiles.Add(Path.GetFileName(item));
        }

        return await Task.FromResult(new GetLogFilesListQueryResult
        {
            LogFiles = logFiles
        });
    }
}