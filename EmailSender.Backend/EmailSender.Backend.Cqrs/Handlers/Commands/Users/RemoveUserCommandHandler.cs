namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System.Threading;
using System.Threading.Tasks;
using Services.UserService;
using Core.Services.LoggerService;
using MediatR;

public class RemoveUserCommandHandler : Cqrs.RequestHandler<RemoveUserCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public RemoveUserCommandHandler(IUserService userService, ILoggerService loggerService)
    {
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        await _userService.RemoveUser(request.UserId, request.SoftDelete, cancellationToken);
        _loggerService.LogInformation($"User account has been deleted, user ID: {request.UserId}");
        return Unit.Value;
    }
}