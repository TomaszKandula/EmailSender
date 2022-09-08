using System;
using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class AddUserEmailInput
{
    public Guid? UserId { get; set; }

    public Guid EmailId { get; set; }
}