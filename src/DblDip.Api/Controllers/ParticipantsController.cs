using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Participants;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/participants")]
    public class ParticipantsController
    {
        private readonly IMediator _mediator;

        public ParticipantsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateParticipantRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateParticipant.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateParticipant.Response>> Create([FromBody]CreateParticipant.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateParticipantRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateParticipant.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateParticipant.Response>> Update([FromBody]UpdateParticipant.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{participantId}", Name = "RemoveParticipantRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveParticipant.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{participantId}", Name = "GetParticipantByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetParticipantById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetParticipantById.Response>> GetById([FromRoute]GetParticipantById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Participant == null)
            {
                return new NotFoundObjectResult(request.ParticipantId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetParticipantsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetParticipants.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetParticipants.Response>> Get()
            => await _mediator.Send(new GetParticipants.Request());           
    }
}
