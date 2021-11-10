namespace EmailSender.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Backend.AppService.Requests;
    using Backend.AppService.Responses;
    using MediatR;

    public class BillingController : BaseController
    {
        public BillingController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<GetUserBillingResponse> GetUserBilling([FromQuery] Guid id, string key) 
            => await Mediator.Send(new GetUserBillingRequest { PrivateKey = key, BillingId = id });

        [HttpGet]
        public async Task<GetAllUserBillingsResponse> GetAllUserBillings([FromQuery] string key) 
            => await Mediator.Send(new GetAllUserBillingsRequest { PrivateKey = key });
    }
}