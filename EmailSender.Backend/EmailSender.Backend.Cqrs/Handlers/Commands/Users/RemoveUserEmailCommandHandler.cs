using System.Threading;
using System.Threading.Tasks;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Services.UserService;
using EmailSender.Services.UserService.Models;
using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

public class RemoveUserEmailCommandHandler : Cqrs.RequestHandler<RemoveUserEmailCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public RemoveUserEmailCommandHandler(IUserService userService, ILoggerService loggerService)
    {
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(RemoveUserEmailCommand request, CancellationToken cancellationToken)
    {
        var input = new RemoveUserEmailInput
        {
            UserId = request.UserId,
            EmailId = request.EmailId
        };

        await _userService.RemoveUserEmail(input, cancellationToken);
        _loggerService.LogInformation($"User email has been removed, user ID: {request.UserId}");

        return Unit.Value;
    }
}