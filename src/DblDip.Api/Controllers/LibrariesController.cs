using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Libraries;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/libraries")]
    public class LibrariesController
    {
        private readonly IMediator _mediator;

        public LibrariesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateLibraryRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateLibrary.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateLibrary.Response>> Create([FromBody] CreateLibrary.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{libraryId}", Name = "RemoveLibraryRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveLibrary.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{libraryId}", Name = "GetLibraryByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetLibraryById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetLibraryById.Response>> GetById([FromRoute] GetLibraryById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Library == null)
            {
                return new NotFoundObjectResult(request.LibraryId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetLibrariesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetLibraries.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetLibraries.Response>> Get()
            => await _mediator.Send(new GetLibraries.Request());
    }
}
