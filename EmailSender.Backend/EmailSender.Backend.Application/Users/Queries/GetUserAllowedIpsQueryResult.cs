namespace EmailSender.Backend.Application.Users.Queries;

public class GetUserAllowedIpsQueryResult
{
    public List<string> IpList { get; set; } = new();
}