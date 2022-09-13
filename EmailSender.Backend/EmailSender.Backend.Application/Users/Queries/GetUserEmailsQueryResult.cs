namespace EmailSender.Backend.Application.Users.Queries;

public class GetUserEmailsQueryResult
{
    public string AssociatedUser { get; set; } = "";

    public List<string> Emails { get; set; } = new();
}