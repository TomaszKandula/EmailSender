using EmailSender.Backend.Application.Mailcow.Models;

namespace EmailSender.Backend.Application.Mailcow;

public class GetMailcowStatusQueryResult
{
    public StatusTypes Status { get; set; }

    public IList<StatusItem>? Results { get; set; }
}