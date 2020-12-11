using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Invoices;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoicesController
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateInvoiceRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateInvoice.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateInvoice.Response>> Create([FromBody]CreateInvoice.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{invoiceId}", Name = "RemoveInvoiceRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveInvoice.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{invoiceId}", Name = "GetInvoiceByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetInvoiceById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetInvoiceById.Response>> GetById([FromRoute]GetInvoiceById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Invoice == null)
            {
                return new NotFoundObjectResult(request.InvoiceId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetInvoicesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetInvoices.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetInvoices.Response>> Get()
            => await _mediator.Send(new GetInvoices.Request());           
    }
}
