namespace EmailSender.Backend.EmailService.Requests
{
    using Responses;
    using MediatR;

    public class GetSentHistoryRequest : IRequest<GetSentHistoryResponse>
    {
        public string PrivateKey { get; set; }
    }
}