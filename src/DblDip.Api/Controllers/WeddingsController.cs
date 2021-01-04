using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/weddings")]
    public class WeddingsController
    {
        private readonly IMediator _mediator;

        public WeddingsController(IMediator mediator) => _mediator = mediator;

        [HttpPost(Name = "CreateWeddingRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateWedding.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateWedding.Response>> Create([FromBody] CreateWedding.Request request)
            => await _mediator.Send(request);
    }
}
