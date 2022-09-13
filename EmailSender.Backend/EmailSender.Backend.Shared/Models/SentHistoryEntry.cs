using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Backend.Shared.Models;

[ExcludeFromCodeCoverage]
public class SentHistoryEntry
{
    public string EmailFrom { get; set; } = "";

    public DateTime SentAt { get; set; }
}