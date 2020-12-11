using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Availabilities;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/availabilities")]
    public class AvailabilitiesController
    {
        private readonly IMediator _mediator;

        public AvailabilitiesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateAvailabilityRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateAvailability.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateAvailability.Response>> Create([FromBody]CreateAvailability.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateAvailabilityRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateAvailability.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateAvailability.Response>> Update([FromBody]UpdateAvailability.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{availabilityId}", Name = "RemoveAvailabilityRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveAvailability.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{availabilityId}", Name = "GetAvailabilityByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAvailabilityById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetAvailabilityById.Response>> GetById([FromRoute]GetAvailabilityById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Availability == null)
            {
                return new NotFoundObjectResult(request.AvailabilityId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetAvailabilitiesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAvailabilities.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAvailabilities.Response>> Get()
            => await _mediator.Send(new GetAvailabilities.Request());           
    }
}
