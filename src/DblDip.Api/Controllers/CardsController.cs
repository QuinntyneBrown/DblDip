using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Cards;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardsController
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateCardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateCard.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateCard.Response>> Create([FromBody]CreateCard.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateCardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateCard.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateCard.Response>> Update([FromBody] UpdateCard.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{cardId}", Name = "RemoveCardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveCard.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{cardId}", Name = "GetCardByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCardById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetCardById.Response>> GetById([FromRoute]GetCardById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Card == null)
            {
                return new NotFoundObjectResult(request.CardId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetCardsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCards.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCards.Response>> Get()
            => await _mediator.Send(new GetCards.Request());           
    }
}
