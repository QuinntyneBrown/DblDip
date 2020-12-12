using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.YouTubeVideos;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/youTubeVideos")]
    public class YouTubeVideosController
    {
        private readonly IMediator _mediator;

        public YouTubeVideosController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateYouTubeVideoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateYouTubeVideo.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateYouTubeVideo.Response>> Create([FromBody] CreateYouTubeVideo.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateYouTubeVideoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateYouTubeVideo.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateYouTubeVideo.Response>> Update([FromBody] UpdateYouTubeVideo.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{youTubeVideoId}", Name = "RemoveYouTubeVideoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveYouTubeVideo.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{youTubeVideoId}", Name = "GetYouTubeVideoByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetYouTubeVideoById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetYouTubeVideoById.Response>> GetById([FromRoute] GetYouTubeVideoById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.YouTubeVideo == null)
            {
                return new NotFoundObjectResult(request.YouTubeVideoId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetYouTubeVideosRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetYouTubeVideos.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetYouTubeVideos.Response>> Get()
            => await _mediator.Send(new GetYouTubeVideos.Request());
    }
}
