using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Meetings;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/meetings")]
    public class MeetingsController
    {
        private readonly IMediator _mediator;

        public MeetingsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateMeetingRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateMeeting.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateMeeting.Response>> Create([FromBody] CreateMeeting.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{meetingId}", Name = "RemoveMeetingRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveMeeting.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{meetingId}", Name = "GetMeetingByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMeetingById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetMeetingById.Response>> GetById([FromRoute] GetMeetingById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Meeting == null)
            {
                return new NotFoundObjectResult(request.MeetingId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetMeetingsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMeetings.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetMeetings.Response>> Get()
            => await _mediator.Send(new GetMeetings.Request());
    }
}
