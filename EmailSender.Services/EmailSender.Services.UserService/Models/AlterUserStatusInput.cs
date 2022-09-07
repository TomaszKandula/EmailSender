namespace EmailSender.Services.UserService.Models;

using System;
using System.Diagnostics.CodeAnalysis;
using Backend.Domain.Enums;

[ExcludeFromCodeCoverage]
public class AlterUserStatusInput
{
    public Guid? UserId { get; set; }

    public UserStatus Status { get; set; }
}