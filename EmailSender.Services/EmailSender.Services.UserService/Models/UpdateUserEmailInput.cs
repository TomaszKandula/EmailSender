namespace EmailSender.Services.UserService.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UpdateUserEmailInput
{
    public Guid UserId { get; set; }

    public Guid OldEmailId { get; set; }
    
    public Guid NewEmailId { get; set; }
}