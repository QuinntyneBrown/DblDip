using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.CorporateEvents;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/corporateEvents")]
    public class CorporateEventsController
    {
        private readonly IMediator _mediator;

        public CorporateEventsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateCorporateEventRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateCorporateEvent.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateCorporateEvent.Response>> Create([FromBody] CreateCorporateEvent.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{corporateEventId}", Name = "RemoveCorporateEventRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveCorporateEvent.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{corporateEventId}", Name = "GetCorporateEventByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCorporateEventById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetCorporateEventById.Response>> GetById([FromRoute] GetCorporateEventById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.CorporateEvent == null)
            {
                return new NotFoundObjectResult(request.CorporateEventId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetCorporateEventsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCorporateEvents.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCorporateEvents.Response>> Get()
            => await _mediator.Send(new GetCorporateEvents.Request());
    }
}
