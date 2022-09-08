using System;
using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserInput : AddUserInput
{
    public Guid? UserId { get; set; }
}