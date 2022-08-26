namespace EmailSender.Backend.Shared.Dto;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public class AlterUserStatusDto
{
    public Guid? UserId { get; set; }

    public UserStatus Status { get; set; }
}