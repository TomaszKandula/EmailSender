using EmailSender.Backend.Application.Mailcow.Models;

namespace EmailSender.Backend.Application.Mailcow;

public class GetMailcowStatusQueryResult
{
    public bool IsRunning { get; set; }

    public IList<Status>? Results { get; set; }
}