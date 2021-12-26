namespace EmailSender.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Backend.Cqrs.Handlers.Queries.Billings;
    using MediatR;

    [ApiVersion("1.0")]
    public class BillingController : BaseController
    {
        public BillingController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(typeof(GetUserBillingQueryResult), StatusCodes.Status200OK)]
        public async Task<GetUserBillingQueryResult> GetUserBilling([FromQuery] Guid id, string key) 
            => await Mediator.Send(new GetUserBillingQuery { PrivateKey = key, BillingId = id });

        [HttpGet]
        [ProducesResponseType(typeof(GetAllUserBillingsQueryResult), StatusCodes.Status200OK)]
        public async Task<GetAllUserBillingsQueryResult> GetAllUserBillings([FromQuery] string key) 
            => await Mediator.Send(new GetAllUserBillingsQuery { PrivateKey = key });
    }
}