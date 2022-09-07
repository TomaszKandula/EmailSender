namespace EmailSender.Services.UserService.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class RemoveUserInput
{
    public Guid? UserId { get; set; }

    public bool SoftDelete { get; set; }
}