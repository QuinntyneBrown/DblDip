using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Posts;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreatePostRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePost.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePost.Response>> Create([FromBody] CreatePost.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdatePostRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdatePost.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdatePost.Response>> Update([FromBody] UpdatePost.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{postId}", Name = "RemovePostRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemovePost.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{postId}", Name = "GetPostByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPostById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetPostById.Response>> GetById([FromRoute] GetPostById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Post == null)
            {
                return new NotFoundObjectResult(request.PostId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetPostsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPosts.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPosts.Response>> Get()
            => await _mediator.Send(new GetPosts.Request());
    }
}
