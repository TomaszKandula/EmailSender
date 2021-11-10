namespace EmailSender.Backend.AppService.Requests
{
    using Responses;
    using MediatR;

    public class GetSentHistoryRequest : IRequest<GetSentHistoryResponse>
    {
        public string PrivateKey { get; set; }
    }
}