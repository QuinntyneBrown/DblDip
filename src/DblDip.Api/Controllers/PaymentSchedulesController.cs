using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.PaymentSchedules;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/paymentSchedules")]
    public class PaymentSchedulesController
    {
        private readonly IMediator _mediator;

        public PaymentSchedulesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreatePaymentScheduleRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePaymentSchedule.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePaymentSchedule.Response>> Create([FromBody]CreatePaymentSchedule.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdatePaymentScheduleRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdatePaymentSchedule.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdatePaymentSchedule.Response>> Update([FromBody]UpdatePaymentSchedule.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{paymentScheduleId}", Name = "RemovePaymentScheduleRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemovePaymentSchedule.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{paymentScheduleId}", Name = "GetPaymentScheduleByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPaymentScheduleById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetPaymentScheduleById.Response>> GetById([FromRoute]GetPaymentScheduleById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.PaymentSchedule == null)
            {
                return new NotFoundObjectResult(request.PaymentScheduleId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetPaymentSchedulesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPaymentSchedules.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPaymentSchedules.Response>> Get()
            => await _mediator.Send(new GetPaymentSchedules.Request());           
    }
}
