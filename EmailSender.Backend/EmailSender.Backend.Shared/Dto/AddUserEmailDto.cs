namespace EmailSender.Backend.Shared.Dto;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AddUserEmailDto
{
    public Guid UserId { get; set; }

    public Guid EmailId { get; set; }
}