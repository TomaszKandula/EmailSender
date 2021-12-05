namespace EmailSender.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Backend.Cqrs.Requests;
    using Backend.Cqrs.Responses;
    using MediatR;

    [ApiVersion("1.0")]
    public class BillingController : BaseController
    {
        public BillingController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(typeof(GetUserBillingQueryResponse), StatusCodes.Status200OK)]
        public async Task<GetUserBillingQueryResponse> GetUserBilling([FromQuery] Guid id, string key) 
            => await Mediator.Send(new GetUserBillingQueryRequest { PrivateKey = key, BillingId = id });

        [HttpGet]
        [ProducesResponseType(typeof(GetAllUserBillingsQueryResponse), StatusCodes.Status200OK)]
        public async Task<GetAllUserBillingsQueryResponse> GetAllUserBillings([FromQuery] string key) 
            => await Mediator.Send(new GetAllUserBillingsQueryRequest { PrivateKey = key });
    }
}