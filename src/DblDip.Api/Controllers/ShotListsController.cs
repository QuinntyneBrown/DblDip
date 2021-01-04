using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/shotLists")]
    public class ShotListsController
    {
        private readonly IMediator _mediator;

        public ShotListsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateShotListRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateShotList.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateShotList.Response>> Create([FromBody] CreateShotList.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateShotListRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateShotList.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateShotList.Response>> Update([FromBody] UpdateShotList.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{shotListId}", Name = "RemoveShotListRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveShotList.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{shotListId}", Name = "GetShotListByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetShotListById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetShotListById.Response>> GetById([FromRoute] GetShotListById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.ShotList == null)
            {
                return new NotFoundObjectResult(request.ShotListId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetShotListsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetShotLists.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetShotLists.Response>> Get()
            => await _mediator.Send(new GetShotLists.Request());
    }
}
