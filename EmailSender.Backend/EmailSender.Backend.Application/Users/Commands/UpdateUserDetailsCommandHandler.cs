using System.Threading;
using System.Threading.Tasks;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Services.UserService;
using EmailSender.Services.UserService.Models;
using MediatR;

namespace EmailSender.Backend.Application.Handlers.Commands.Users;

public class UpdateUserDetailsCommandHandler : RequestHandler<UpdateUserDetailsCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ILoggerService _loggerService;

    public UpdateUserDetailsCommandHandler(ILoggerService loggerService, IUserService userService)
    {
        _loggerService = loggerService;
        _userService = userService;
    }

    public override async Task<Unit> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var input = new UpdateUserDetailsInput
        {
            UserId = request.UserId,
            CompanyName = request.CompanyName, 
            VatNumber = request.VatNumber,
            StreetAddress = request.StreetAddress,
            PostalCode = request.PostalCode,
            Country = request.Country,
            City= request.City
        };

        await _userService.UpdateUserDetails(input, cancellationToken);
        _loggerService.LogInformation($"User account has been updated, user ID: {request.UserId}");

        return Unit.Value;
    }
}