namespace EmailSender.Backend.Application.Users.Queries;

public class GetUserAllowedIpsQueryResult
{
    public IEnumerable<string> IpList { get; set; }
}