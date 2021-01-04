using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/epics")]
    public class EpicsController
    {
        private readonly IMediator _mediator;

        public EpicsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateEpicRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateEpic.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateEpic.Response>> Create([FromBody] CreateEpic.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateEpicRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateEpic.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateEpic.Response>> Update([FromBody] UpdateEpic.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{epicId}", Name = "RemoveEpicRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveEpic.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{epicId}", Name = "GetEpicByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetEpicById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetEpicById.Response>> GetById([FromRoute] GetEpicById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Epic == null)
            {
                return new NotFoundObjectResult(request.EpicId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetEpicsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetEpics.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetEpics.Response>> Get()
            => await _mediator.Send(new GetEpics.Request());
    }
}
