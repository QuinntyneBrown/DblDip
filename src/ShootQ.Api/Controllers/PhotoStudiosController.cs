using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.PhotoStudios;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/photoStudios")]
    public class PhotoStudiosController
    {
        private readonly IMediator _mediator;

        public PhotoStudiosController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreatePhotoStudioRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePhotoStudio.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePhotoStudio.Response>> Create([FromBody]CreatePhotoStudio.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{photoStudioId}", Name = "RemovePhotoStudioRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemovePhotoStudio.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{photoStudioId}", Name = "GetPhotoStudioByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPhotoStudioById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetPhotoStudioById.Response>> GetById([FromRoute]GetPhotoStudioById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.PhotoStudio == null)
            {
                return new NotFoundObjectResult(request.PhotoStudioId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetPhotoStudiosRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPhotoStudios.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPhotoStudios.Response>> Get()
            => await _mediator.Send(new GetPhotoStudios.Request());           
    }
}
