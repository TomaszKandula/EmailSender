using System;
using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Backend.Shared.Models;

[ExcludeFromCodeCoverage]
public class RequestHistoryEntry
{
    public string Action { get; set; }

    public DateTime RequestedAt { get; set; }
}