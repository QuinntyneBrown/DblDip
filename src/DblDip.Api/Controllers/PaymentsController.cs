using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Payments;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreatePaymentRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePayment.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePayment.Response>> Create([FromBody] CreatePayment.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdatePaymentRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdatePayment.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdatePayment.Response>> Update([FromBody] UpdatePayment.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{paymentId}", Name = "RemovePaymentRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemovePayment.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{paymentId}", Name = "GetPaymentByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPaymentById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetPaymentById.Response>> GetById([FromRoute] GetPaymentById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Payment == null)
            {
                return new NotFoundObjectResult(request.PaymentId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetPaymentsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPayments.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPayments.Response>> Get()
            => await _mediator.Send(new GetPayments.Request());
    }
}
