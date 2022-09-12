using System;
using MediatR;

namespace EmailSender.Backend.Application.Users.Commands;

public class GeneratePrivateKeyCommand : IRequest<string>
{
    public Guid? UserId { get; set; }
}