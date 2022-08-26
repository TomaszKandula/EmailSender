namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System.Threading;
using System.Threading.Tasks;
using Services.UserService;
using Services.UserService.Models;
using Core.Services.LoggerService;
using MediatR;

public class AlterUserStatusCommandHandler : Cqrs.RequestHandler<AlterUserStatusCommand, Unit>
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