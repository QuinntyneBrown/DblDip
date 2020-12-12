using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Offers;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public class OffersController
    {
        private readonly IMediator _mediator;

        public OffersController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateOfferRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateOffer.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateOffer.Response>> Create([FromBody] CreateOffer.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateOfferRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateOffer.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateOffer.Response>> Update([FromBody] UpdateOffer.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{offerId}", Name = "RemoveOfferRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveOffer.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{offerId}", Name = "GetOfferByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOfferById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetOfferById.Response>> GetById([FromRoute] GetOfferById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Offer == null)
            {
                return new NotFoundObjectResult(request.OfferId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetOffersRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOffers.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetOffers.Response>> Get()
            => await _mediator.Send(new GetOffers.Request());
    }
}
