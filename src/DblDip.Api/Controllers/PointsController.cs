using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Points;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/points")]
    public class PointsController
    {
        private readonly IMediator _mediator;

        public PointsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreatePointRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePoint.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePoint.Response>> Create([FromBody] CreatePoint.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdatePointRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdatePoint.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdatePoint.Response>> Update([FromBody] UpdatePoint.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{pointId}", Name = "RemovePointRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemovePoint.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{pointId}", Name = "GetPointByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPointById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetPointById.Response>> GetById([FromRoute] GetPointById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Point == null)
            {
                return new NotFoundObjectResult(request.PointId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetPointsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPoints.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPoints.Response>> Get()
            => await _mediator.Send(new GetPoints.Request());
    }
}
