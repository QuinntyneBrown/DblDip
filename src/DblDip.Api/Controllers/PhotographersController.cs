using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/photographers")]
    public class PhotographersController
    {
        private readonly IMediator _mediator;

        public PhotographersController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreatePhotographerRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePhotographer.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePhotographer.Response>> Create([FromBody] CreatePhotographer.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{photographerId}", Name = "RemovePhotographerRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemovePhotographer.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{photographerId}", Name = "GetPhotographerByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPhotographerById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetPhotographerById.Response>> GetById([FromRoute] GetPhotographerById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Photographer == null)
            {
                return new NotFoundObjectResult(request.PhotographerId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetPhotographersRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPhotographers.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPhotographers.Response>> Get()
            => await _mediator.Send(new GetPhotographers.Request());
    }
}
