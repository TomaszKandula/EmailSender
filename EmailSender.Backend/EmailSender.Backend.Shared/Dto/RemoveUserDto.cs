namespace EmailSender.Backend.Shared.Dto;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class RemoveUserDto
{
    public Guid? UserId { get; set; }

    public bool SoftDelete { get; set; }
}