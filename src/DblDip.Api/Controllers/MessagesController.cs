using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateMessageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateMessage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateMessage.Response>> Create([FromBody] CreateMessage.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateMessageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateMessage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateMessage.Response>> Update([FromBody] UpdateMessage.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{messageId}", Name = "RemoveMessageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveMessage.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{messageId}", Name = "GetMessageByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMessageById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetMessageById.Response>> GetById([FromRoute] GetMessageById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Message == null)
            {
                return new NotFoundObjectResult(request.MessageId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetMessagesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMessages.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetMessages.Response>> Get()
            => await _mediator.Send(new GetMessages.Request());
    }
}
