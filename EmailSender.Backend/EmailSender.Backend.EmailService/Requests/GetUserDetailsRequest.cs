namespace EmailSender.Backend.EmailService.Requests
{
    using MediatR;
    using Responses;

    public class GetUserDetailsRequest : IRequest<GetUserDetailsResponse>
    {
        public string PrivateKey { get; set; }
    }
}