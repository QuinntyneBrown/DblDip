using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/stories")]
    public class StoriesController
    {
        private readonly IMediator _mediator;

        public StoriesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateStoryRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateStory.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateStory.Response>> Create([FromBody]CreateStory.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateStoryRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateStory.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateStory.Response>> Update([FromBody]UpdateStory.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{storyId}", Name = "RemoveStoryRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveStory.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{storyId}", Name = "GetStoryByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetStoryById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetStoryById.Response>> GetById([FromRoute]GetStoryById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Story == null)
            {
                return new NotFoundObjectResult(request.StoryId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetStoriesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetStories.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetStories.Response>> Get()
            => await _mediator.Send(new GetStories.Request());           
    }
}
