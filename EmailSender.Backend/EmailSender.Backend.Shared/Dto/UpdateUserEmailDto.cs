namespace EmailSender.Backend.Shared.Dto;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UpdateUserEmailDto
{
    public Guid? UserId { get; set; }

    public Guid OldEmailId { get; set; }

    public Guid NewEmailId { get; set; }
}