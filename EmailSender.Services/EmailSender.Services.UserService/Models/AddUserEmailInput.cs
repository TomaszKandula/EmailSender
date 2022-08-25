namespace EmailSender.Services.UserService.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AddUserEmailInput
{
    public Guid UserId { get; set; }

    public Guid EmailId { get; set; }
}