using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Venues;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/venues")]
    public class VenuesController
    {
        private readonly IMediator _mediator;

        public VenuesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateVenueRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateVenue.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateVenue.Response>> Create([FromBody]CreateVenue.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{venueId}", Name = "RemoveVenueRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveVenue.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{venueId}", Name = "GetVenueByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVenueById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetVenueById.Response>> GetById([FromRoute]GetVenueById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Venue == null)
            {
                return new NotFoundObjectResult(request.VenueId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetVenuesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVenues.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetVenues.Response>> Get()
            => await _mediator.Send(new GetVenues.Request());           
    }
}
