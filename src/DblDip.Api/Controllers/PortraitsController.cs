using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/portraits")]
    public class PortraitsController
    {
        private readonly IMediator _mediator;

        public PortraitsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreatePortraitRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePortrait.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePortrait.Response>> Create([FromBody] CreatePortrait.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{portraitId}", Name = "RemovePortraitRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemovePortrait.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{portraitId}", Name = "GetPortraitByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPortraitById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetPortraitById.Response>> GetById([FromRoute] GetPortraitById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Portrait == null)
            {
                return new NotFoundObjectResult(request.PortraitId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetPortraitsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPortraits.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPortraits.Response>> Get()
            => await _mediator.Send(new GetPortraits.Request());
    }
}
