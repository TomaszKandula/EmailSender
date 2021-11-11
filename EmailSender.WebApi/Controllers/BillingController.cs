namespace EmailSender.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Backend.Cqrs.Requests;
    using Backend.Cqrs.Responses;
    using MediatR;

    public class BillingController : BaseController
    {
        public BillingController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<GetUserBillingQueryResponse> GetUserBilling([FromQuery] Guid id, string key) 
            => await Mediator.Send(new GetUserBillingQueryRequest { PrivateKey = key, BillingId = id });

        [HttpGet]
        public async Task<GetAllUserBillingsQueryResponse> GetAllUserBillings([FromQuery] string key) 
            => await Mediator.Send(new GetAllUserBillingsQueryRequest { PrivateKey = key });
    }
}