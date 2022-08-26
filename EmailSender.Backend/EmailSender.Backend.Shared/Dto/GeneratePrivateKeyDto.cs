namespace EmailSender.Backend.Shared.Dto;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class GeneratePrivateKeyDto
{
    public Guid? UserId { get; set; }
}