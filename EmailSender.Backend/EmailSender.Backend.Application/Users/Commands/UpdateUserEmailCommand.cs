using MediatR;

namespace EmailSender.Backend.Application.Users.Commands;

public class UpdateUserEmailCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public Guid OldEmailId { get; set; }

    public Guid NewEmailId { get; set; }
}