using Newtonsoft.Json;

namespace EmailSender.Backend.Application.Mailcow.Models;

public class StatusItem
{
    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("container")]
    public string? Container { get; set; }

    [JsonProperty("state")]
    public string? State { get; set; }

    [JsonProperty("started_at")]
    public DateTime? StartedAt { get; set; }

    [JsonProperty("image")]
    public string? Image { get; set; }
}