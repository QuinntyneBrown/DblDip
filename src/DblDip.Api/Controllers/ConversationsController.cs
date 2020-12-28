using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Conversations;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/conversations")]
    public class ConversationsController
    {
        private readonly IMediator _mediator;

        public ConversationsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateConversationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateConversation.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateConversation.Response>> Create([FromBody] CreateConversation.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateConversationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateConversation.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateConversation.Response>> Update([FromBody] UpdateConversation.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{conversationId}", Name = "RemoveConversationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveConversation.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{conversationId}", Name = "GetConversationByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetConversationById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetConversationById.Response>> GetById([FromRoute] GetConversationById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Conversation == null)
            {
                return new NotFoundObjectResult(request.ConversationId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetConversationsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetConversations.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetConversations.Response>> Get()
            => await _mediator.Send(new GetConversations.Request());
    }
}
