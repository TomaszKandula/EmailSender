namespace EmailSender.Services.UserService.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserInfo : UserData
{
    public Guid UserId { get; set; }
}