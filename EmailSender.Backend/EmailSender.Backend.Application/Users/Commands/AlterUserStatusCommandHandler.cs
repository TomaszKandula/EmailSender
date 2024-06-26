using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Services.UserService;
using EmailSender.Services.UserService.Models;
using MediatR;

namespace EmailSender.Backend.Application.Users.Commands;

public class AlterUserStatusCommandHandler : RequestHandler<AlterUserStatusCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public AlterUserStatusCommandHandler(IUserService userService, ILoggerService loggerService)
    {
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(AlterUserStatusCommand request, CancellationToken cancellationToken)
    {
        var input = new AlterUserStatusInput
        {
            UserId = request.UserId,
            Status = request.Status
        };

        await _userService.AlterUserStatus(input, cancellationToken);
        _loggerService.LogInformation($"User status has been changed to {request.Status}");
        return Unit.Value;
    }
}