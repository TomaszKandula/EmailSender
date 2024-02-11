namespace EmailSender.Backend.Application.Mailcow.Models;

public class Status
{
    public string? Container { get; set; }

    public string? State { get; set; }

    public bool? StartedAt { get; set; }
}