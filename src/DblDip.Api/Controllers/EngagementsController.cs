using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Engagements;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/engagements")]
    public class EngagementsController
    {
        private readonly IMediator _mediator;

        public EngagementsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateEngagementRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateEngagement.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateEngagement.Response>> Create([FromBody]CreateEngagement.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{engagementId}", Name = "RemoveEngagementRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveEngagement.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{engagementId}", Name = "GetEngagementByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetEngagementById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetEngagementById.Response>> GetById([FromRoute]GetEngagementById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Engagement == null)
            {
                return new NotFoundObjectResult(request.EngagementId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetEngagementsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetEngagements.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetEngagements.Response>> Get()
            => await _mediator.Send(new GetEngagements.Request());           
    }
}
