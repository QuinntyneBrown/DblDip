using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/systemLocations")]
    public class SystemLocationsController
    {
        private readonly IMediator _mediator;

        public SystemLocationsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateSystemLocationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateSystemLocation.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateSystemLocation.Response>> Create([FromBody] CreateSystemLocation.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{systemLocationId}", Name = "RemoveSystemLocationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveSystemLocation.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{systemLocationId}", Name = "GetSystemLocationByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSystemLocationById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetSystemLocationById.Response>> GetById([FromRoute] GetSystemLocationById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.SystemLocation == null)
            {
                return new NotFoundObjectResult(request.SystemLocationId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetSystemLocationsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSystemLocations.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSystemLocations.Response>> Get()
            => await _mediator.Send(new GetSystemLocations.Request());
    }
}
