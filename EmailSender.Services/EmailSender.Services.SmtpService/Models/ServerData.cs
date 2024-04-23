using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Services.SmtpService.Models;

[ExcludeFromCodeCoverage]
public class ServerData
{
    public string Address { get; set; } = "";

    public string Server { get; set; } = "";

    public string Key { get; set; } = "";

    public int Port { get; set; }

    public bool IsSsl { get; set; }
}