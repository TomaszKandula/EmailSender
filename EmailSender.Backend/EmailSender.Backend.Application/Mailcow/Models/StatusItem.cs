using System.Text.Json.Serialization;

namespace EmailSender.Backend.Application.Mailcow.Models;

public class StatusItem
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("container")]
    public string? Container { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("started_at")]
    public DateTime? StartedAt { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }
}