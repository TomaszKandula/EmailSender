namespace EmailSender.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Backend.EmailService.Requests;
    using Backend.EmailService.Responses;
    using MediatR;

    public class BillingControllers : BaseController
    {
        public BillingControllers(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<GetUserBillingResponse> GetUserBilling([FromQuery] Guid id, string key) 
            => await Mediator.Send(new GetUserBillingRequest { PrivateKey = key, BillingId = id });
    }
}