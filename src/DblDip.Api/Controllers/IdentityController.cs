using MediatR;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("quote", Name = "UserAuthenticateByEmailAndQuoteIdnRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Authenticate.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthenticateByEmailAndQuoteId.Response>> TokenByEmailAndQuoteId([FromBody] AuthenticateByEmailAndQuoteId.Request request)
        {
            return await _mediator.Send(request);
        }


        [HttpPost("token", Name = "UserAuthenticateRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Authenticate.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Authenticate.Response>> Token([FromBody] Authenticate.Request request)
        {
            return await _mediator.Send(request);
        }



        [HttpPost("refresh", Name = "UserRefreshRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Authenticate.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Refresh.Response>> Refresh([FromBody] Refresh.Request request)
        {
            var response = await _mediator.Send(request);

            if (response == null)
            {
                return new UnauthorizedResult();
            }

            return response;
        }
    }
}
