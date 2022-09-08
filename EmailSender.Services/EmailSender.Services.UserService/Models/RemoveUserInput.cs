using System;
using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class RemoveUserInput
{
    public Guid? UserId { get; set; }

    public bool SoftDelete { get; set; }
}