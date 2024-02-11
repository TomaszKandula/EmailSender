namespace EmailSender.Backend.Application.Mailcow;

public class GetMailcowStatusQueryHandler : RequestHandler<GetMailcowStatusQuery, GetMailcowStatusQueryResult>
{
    public override async Task<GetMailcowStatusQueryResult> Handle(GetMailcowStatusQuery request, CancellationToken cancellationToken)
    {




        return new GetMailcowStatusQueryResult();
    }
}