using System.Threading;
using System.Threading.Tasks;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Services.UserService;
using EmailSender.Services.UserService.Models;
using MediatR;

namespace EmailSender.Backend.Application.Handlers.Commands.Users;

public class RemoveUserCommandHandler : RequestHandler<RemoveUserCommand, Unit>
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
        var input = new RemoveUserInput
        {
            UserId = request.UserId,
            SoftDelete = request.SoftDelete
        };

        await _userService.RemoveUser(input, cancellationToken);
        _loggerService.LogInformation($"User account has been deleted, user ID: {request.UserId}");
        return Unit.Value;
    }
}