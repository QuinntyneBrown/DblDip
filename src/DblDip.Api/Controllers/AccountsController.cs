using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Accounts;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPut("current-profile",Name = "SetCurrentProfileRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SetCurrentProfile.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SetCurrentProfile.Response>> CurrentProfile([FromBody] SetCurrentProfile.Request request)
            => await _mediator.Send(request);

    }
}
