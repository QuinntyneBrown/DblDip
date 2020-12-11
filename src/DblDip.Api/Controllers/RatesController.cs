using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Rates;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/rates")]
    public class RatesController
    {
        private readonly IMediator _mediator;

        public RatesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateRateRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateRate.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateRate.Response>> Create([FromBody] CreateRate.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{rateId}", Name = "RemoveRateRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveRate.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{rateId}", Name = "GetRateByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetRateById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetRateById.Response>> GetById([FromRoute] GetRateById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Rate == null)
            {
                return new NotFoundObjectResult(request.RateId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetRatesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetRates.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetRates.Response>> Get()
            => await _mediator.Send(new GetRates.Request());
    }
}
