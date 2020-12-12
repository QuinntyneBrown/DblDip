using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Boards;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/boards")]
    public class BoardsController
    {
        private readonly IMediator _mediator;

        public BoardsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateBoardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateBoard.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateBoard.Response>> Create([FromBody]CreateBoard.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateBoardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateBoard.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateBoard.Response>> Update([FromBody]UpdateBoard.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{boardId}", Name = "RemoveBoardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveBoard.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{boardId}", Name = "GetBoardByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBoardById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetBoardById.Response>> GetById([FromRoute]GetBoardById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Board == null)
            {
                return new NotFoundObjectResult(request.BoardId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetBoardsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBoards.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBoards.Response>> Get()
            => await _mediator.Send(new GetBoards.Request());           
    }
}
