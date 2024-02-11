using System.Reflection;
using EmailSender.Backend.Application.Mailcow.Models;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Services.HttpClientService.Abstractions;
using EmailSender.Services.HttpClientService.Models;
using Microsoft.Extensions.Configuration;

namespace EmailSender.Backend.Application.Mailcow;

public class GetMailcowStatusQueryHandler : RequestHandler<GetMailcowStatusQuery, GetMailcowStatusQueryResult>
{
    private readonly IHttpClientServiceFactory _httpClientServiceFactory;

    private readonly ILoggerService _loggerService;

    private readonly IConfiguration _configuration;

    private const BindingFlags Flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField;

    public GetMailcowStatusQueryHandler(IHttpClientServiceFactory httpClientServiceFactory, ILoggerService loggerService, IConfiguration configuration)
    {
        _httpClientServiceFactory = httpClientServiceFactory;
        _loggerService = loggerService;
        _configuration = configuration;
    }

    public override async Task<GetMailcowStatusQueryResult> Handle(GetMailcowStatusQuery request, CancellationToken cancellationToken)
    {
        var headers = new Dictionary<string, string>
        {
            ["X-API-Key"] = _configuration.GetValue<string>("Mailcow_API_Key")
        };

        var configuration = new Configuration
        { 
            Url = _configuration.GetValue<string>("Mailcow_Status_Url"), 
            Method = "GET", 
            Headers = headers
        };

        var client = _httpClientServiceFactory.Create(false, _loggerService);
        var result = await client.Execute<MailcowStatus>(configuration, cancellationToken);

        var healthyCount = 0;
        var unhealthyCount = 0;
        var data = new List<StatusItem>();

        var resultType = result.GetType();
        var resultProps = resultType.GetProperties(Flags);

        foreach (var prop in resultProps)
        {
            var item = prop.GetValue(result, null);
            if (item is null)
                continue;

            var statusItem = item as StatusItem;
            data.Add(statusItem!);

            if (statusItem?.State != "running")
            {
                healthyCount += 1;
            }
            else
            {
                unhealthyCount += 1;
            }
        }

        if (healthyCount == data.Count)
        {
            return new GetMailcowStatusQueryResult
            {
                Status = StatusTypes.Healthy,
                Results = data
            }; 
        }

        if (healthyCount != unhealthyCount)
        {
            return new GetMailcowStatusQueryResult
            {
                Status = StatusTypes.Degraded,
                Results = data
            }; 
        }

        if (healthyCount == 0)
        {
            return new GetMailcowStatusQueryResult
            {
                Status = StatusTypes.Unhealthy,
                Results = data
            }; 
        }

        return new GetMailcowStatusQueryResult
        {
            Status = StatusTypes.Unknown,
            Results = data
        };
    }
}