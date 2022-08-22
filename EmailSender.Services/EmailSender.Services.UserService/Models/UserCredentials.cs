namespace EmailSender.Services.UserService.Models;

using System;
using System.Diagnostics.CodeAnalysis;
using Backend.Domain.Enums;

[ExcludeFromCodeCoverage]
public class UserCredentials
{
    public Guid UserId { get; set; }

    public string PrivateKey { get; set; }

    public string UserAlias { get; set; }

    public string EmailAddress { get; set; }

    public UserStatus Status { get; set; }
}