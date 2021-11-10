namespace EmailSender.Backend.AppService.Requests
{
    using Responses;
    using MediatR;

    public class GetAllowEmailsRequest : IRequest<GetAllowEmailsResponse>
    {
        public string PrivateKey { get; set; }
    }
}