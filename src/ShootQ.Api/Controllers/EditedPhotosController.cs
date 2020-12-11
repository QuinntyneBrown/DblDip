using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.EditedPhotos;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/editedPhotos")]
    public class EditedPhotosController
    {
        private readonly IMediator _mediator;

        public EditedPhotosController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateEditedPhotoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateEditedPhoto.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateEditedPhoto.Response>> Create([FromBody]CreateEditedPhoto.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{editedPhotoId}", Name = "RemoveEditedPhotoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveEditedPhoto.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{editedPhotoId}", Name = "GetEditedPhotoByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetEditedPhotoById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetEditedPhotoById.Response>> GetById([FromRoute]GetEditedPhotoById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.EditedPhoto == null)
            {
                return new NotFoundObjectResult(request.EditedPhotoId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetEditedPhotosRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetEditedPhotos.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetEditedPhotos.Response>> Get()
            => await _mediator.Send(new GetEditedPhotos.Request());           
    }
}
