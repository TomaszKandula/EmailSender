namespace EmailSender.Backend.AppService.Requests
{
    using MediatR;
    using Responses;

    public class GetUserDetailsRequest : IRequest<GetUserDetailsResponse>
    {
        public string PrivateKey { get; set; }
    }
}