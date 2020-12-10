using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.SocialEvents;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/socialEvents")]
    public class SocialEventsController
    {
        private readonly IMediator _mediator;

        public SocialEventsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateSocialEventRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateSocialEvent.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateSocialEvent.Response>> Create([FromBody] CreateSocialEvent.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{socialEventId}", Name = "RemoveSocialEventRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveSocialEvent.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{socialEventId}", Name = "GetSocialEventByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSocialEventById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetSocialEventById.Response>> GetById([FromRoute] GetSocialEventById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.SocialEvent == null)
            {
                return new NotFoundObjectResult(request.SocialEventId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetSocialEventsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSocialEvents.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSocialEvents.Response>> Get()
            => await _mediator.Send(new GetSocialEvents.Request());
    }
}
