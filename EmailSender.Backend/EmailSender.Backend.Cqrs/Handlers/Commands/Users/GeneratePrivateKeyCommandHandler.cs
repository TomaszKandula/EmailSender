using System.Threading;
using System.Threading.Tasks;
using EmailSender.Backend.Core.Services.LoggerService;
using EmailSender.Services.UserService;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

public class GeneratePrivateKeyCommandHandler : RequestHandler<GeneratePrivateKeyCommand, string>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public GeneratePrivateKeyCommandHandler(IUserService userService, ILoggerService loggerService)
    {
        _userService = userService;
        _loggerService = loggerService;
    }

    public override async Task<string> Handle(GeneratePrivateKeyCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.GeneratePrivateKey(request.UserId, cancellationToken);

        _loggerService.LogInformation(request.UserId is null
            ? "New private key generated"
            : $"New private key generated and saved for the user ID: {request.UserId}");

        return result;
    }
}