using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketsController
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateTicketRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateTicket.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateTicket.Response>> Create([FromBody] CreateTicket.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateTicketRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateTicket.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateTicket.Response>> Update([FromBody] UpdateTicket.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{ticketId}", Name = "RemoveTicketRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveTicket.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{ticketId}", Name = "GetTicketByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTicketById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetTicketById.Response>> GetById([FromRoute] GetTicketById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Ticket == null)
            {
                return new NotFoundObjectResult(request.TicketId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetTicketsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTickets.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTickets.Response>> Get()
            => await _mediator.Send(new GetTickets.Request());
    }
}
