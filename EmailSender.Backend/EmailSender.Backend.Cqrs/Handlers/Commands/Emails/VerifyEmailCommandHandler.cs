namespace EmailSender.Backend.Cqrs.Handlers.Commands.Emails;

using System.Threading;
using System.Threading.Tasks;
using Database;
using Domain.Entities;
using Services.UserService;
using Services.SenderService;
using Core.Services.DateTimeService;

public class VerifyEmailCommandHandler : RequestHandler<VerifyEmailCommand, VerifyEmailCommandResult>
{
    private readonly DatabaseContext _databaseContext;

    private readonly IUserService _userService;

    private readonly ISenderService _senderService;

    private readonly IDateTimeService _dateTimeService;

    public VerifyEmailCommandHandler(DatabaseContext databaseContext, IUserService userService,
        ISenderService senderService, IDateTimeService dateTimeService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _senderService = senderService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<VerifyEmailCommandResult> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserByPrivateKey(_userService.GetPrivateKeyFromHeader(), cancellationToken);
        var apiRequest = new RequestsHistory
        {
            UserId = userId,
            Requested = _dateTimeService.Now,
            RequestName = nameof(VerifyEmailCommand)
        };

        await _databaseContext.AddAsync(apiRequest, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        var result = await _senderService.VerifyEmailAddress(request.Emails, cancellationToken);

        return new VerifyEmailCommandResult
        {
            CheckResult = result
        };
    }
}