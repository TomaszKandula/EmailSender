namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System.Threading;
using System.Threading.Tasks;
using Services.UserService;
using Services.UserService.Models;
using Backend.Core.Services.LoggerService;
using MediatR;

public class UpdateUserCommandHandler : Cqrs.RequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public UpdateUserCommandHandler(IUserService userService, ILoggerService loggerService)
    {
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var input = new UpdateUserInput
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailAddress = request.EmailAddress
        };

        await _userService.UpdateUser(input, cancellationToken);
        _loggerService.LogInformation("User account has been updated.");

        return  Unit.Value;
    }
}