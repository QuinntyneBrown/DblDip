using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.PhotoGalleries;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/photoGalleries")]
    public class PhotoGalleriesController
    {
        private readonly IMediator _mediator;

        public PhotoGalleriesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreatePhotoGalleryRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePhotoGallery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePhotoGallery.Response>> Create([FromBody] CreatePhotoGallery.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{photoGalleryId}", Name = "RemovePhotoGalleryRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemovePhotoGallery.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{photoGalleryId}", Name = "GetPhotoGalleryByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPhotoGalleryById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetPhotoGalleryById.Response>> GetById([FromRoute] GetPhotoGalleryById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.PhotoGallery == null)
            {
                return new NotFoundObjectResult(request.PhotoGalleryId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetPhotoGalleriesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPhotoGalleries.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPhotoGalleries.Response>> Get()
            => await _mediator.Send(new GetPhotoGalleries.Request());
    }
}
