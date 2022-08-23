namespace EmailSender.Backend.Shared.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class RequestHistoryEntry
{
    public string Action { get; set; }

    public DateTime SentAt { get; set; }
}