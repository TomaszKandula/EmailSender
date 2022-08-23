namespace EmailSender.Backend.Shared.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SentHistoryEntry
{
    public string EmailFrom { get; set; }
        
    public DateTime SentAt { get; set; }
}