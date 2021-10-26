namespace EmailSenderService.Backend.EmailService.Requests
{
    using Responses;
    using MediatR;

    public class GetAllowEmailsRequest : IRequest<GetAllowEmailsResponse>
    {
        public string PrivateKey { get; set; }
    }
}