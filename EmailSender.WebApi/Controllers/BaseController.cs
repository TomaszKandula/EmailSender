namespace EmailSender.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using MediatR;

    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public BaseController(IMediator mediator) => Mediator = mediator;
    }
}