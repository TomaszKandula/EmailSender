namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System.Threading;
using System.Threading.Tasks;
using Services.UserService;
using Core.Services.LoggerService;
using MediatR;

public class AddUserEmailCommandHandler : Cqrs.RequestHandler<AddUserEmailCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public AddUserEmailCommandHandler(IUserService userService, ILoggerService loggerService)
    {
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(AddUserEmailCommand request, CancellationToken cancellationToken)
    {
        await _userService.AddUserEmail(request.UserId, request.EmailId, cancellationToken);
        _loggerService.LogInformation($"New user email has been added, user ID: {request.UserId}");
        return Unit.Value;
    }
}