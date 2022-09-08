using System;
using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserEmailInput
{
    public Guid? UserId { get; set; }

    public Guid OldEmailId { get; set; }
    
    public Guid NewEmailId { get; set; }
}