using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Blogs;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/blogs")]
    public class BlogsController
    {
        private readonly IMediator _mediator;

        public BlogsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateBlogRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateBlog.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateBlog.Response>> Create([FromBody]CreateBlog.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateBlogRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateBlog.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateBlog.Response>> Update([FromBody]UpdateBlog.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{blogId}", Name = "RemoveBlogRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveBlog.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{blogId}", Name = "GetBlogByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBlogById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetBlogById.Response>> GetById([FromRoute]GetBlogById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Blog == null)
            {
                return new NotFoundObjectResult(request.BlogId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetBlogsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBlogs.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBlogs.Response>> Get()
            => await _mediator.Send(new GetBlogs.Request());           
    }
}
