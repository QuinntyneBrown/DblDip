using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.WeddingQuotes;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/weddingQuotes")]
    public class WeddingQuotesController
    {
        private readonly IMediator _mediator;

        public WeddingQuotesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateWeddingQuoteRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateWeddingQuote.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateWeddingQuote.Response>> Create([FromBody] CreateWeddingQuote.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{weddingQuoteId}", Name = "RemoveWeddingQuoteRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveWeddingQuote.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{weddingQuoteId}", Name = "GetWeddingQuoteByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetWeddingQuoteById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetWeddingQuoteById.Response>> GetById([FromRoute] GetWeddingQuoteById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.WeddingQuote == null)
            {
                return new NotFoundObjectResult(request.WeddingQuoteId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetWeddingQuotesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetWeddingQuotes.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetWeddingQuotes.Response>> Get()
            => await _mediator.Send(new GetWeddingQuotes.Request());
    }
}
