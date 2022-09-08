using System.Threading;
using System.Threading.Tasks;
using EmailSender.Backend.Core.Services.LoggerService;
using EmailSender.Services.UserService;
using EmailSender.Services.UserService.Models;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

public class AddUserCommandHandler : RequestHandler<AddUserCommand, AddUserCommandResult>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public AddUserCommandHandler(IUserService userService, ILoggerService loggerService)
    {
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<AddUserCommandResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var input = new AddUserInput
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailAddress = request.EmailAddress
        };

        var result = await _userService.AddUser(input, cancellationToken);
        _loggerService.LogInformation($"New user account has been added. User ID: {result.UserId}");
        _loggerService.LogInformation($"Current user status: {result.Status}");

        return new AddUserCommandResult
        {
            UserId = result.UserId,
            PrivateKey = result.PrivateKey,
            UserAlias = result.UserAlias,
            EmailAddress = result.EmailAddress,
            Status = result.Status
        };
    }
}