using MediatR;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Quotes;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/quotes")]
    public class QuotesController
    {
        private readonly IMediator _mediator;

        public QuotesController(IMediator mediator) => _mediator = mediator;

        [HttpPost("wedding", Name = "QuoteWeddingRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateWeddingQuote.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CreateWeddingQuote.Response>> Quote([FromBody] CreateWeddingQuote.Request request)
            => await _mediator.Send(request);
    }
}
