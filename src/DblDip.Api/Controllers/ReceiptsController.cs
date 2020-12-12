using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Receipts;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/receipts")]
    public class ReceiptsController
    {
        private readonly IMediator _mediator;

        public ReceiptsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateReceiptRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateReceipt.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateReceipt.Response>> Create([FromBody]CreateReceipt.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateReceiptRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateReceipt.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateReceipt.Response>> Update([FromBody]UpdateReceipt.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{receiptId}", Name = "RemoveReceiptRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveReceipt.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{receiptId}", Name = "GetReceiptByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetReceiptById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetReceiptById.Response>> GetById([FromRoute]GetReceiptById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Receipt == null)
            {
                return new NotFoundObjectResult(request.ReceiptId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetReceiptsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetReceipts.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetReceipts.Response>> Get()
            => await _mediator.Send(new GetReceipts.Request());           
    }
}
