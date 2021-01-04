using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/timeEntries")]
    public class TimeEntriesController
    {
        private readonly IMediator _mediator;

        public TimeEntriesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateTimeEntryRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateTimeEntry.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateTimeEntry.Response>> Create([FromBody] CreateTimeEntry.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{timeEntryId}", Name = "RemoveTimeEntryRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveTimeEntry.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{timeEntryId}", Name = "GetTimeEntryByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTimeEntryById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetTimeEntryById.Response>> GetById([FromRoute] GetTimeEntryById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.TimeEntry == null)
            {
                return new NotFoundObjectResult(request.TimeEntryId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetTimeEntriesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTimeEntries.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTimeEntries.Response>> Get()
            => await _mediator.Send(new GetTimeEntries.Request());
    }
}
