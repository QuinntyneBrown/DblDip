using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/discounts")]
    public class DiscountsController
    {
        private readonly IMediator _mediator;

        public DiscountsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateDiscountRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateDiscount.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateDiscount.Response>> Create([FromBody]CreateDiscount.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateDiscountRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateDiscount.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateDiscount.Response>> Update([FromBody]UpdateDiscount.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{discountId}", Name = "RemoveDiscountRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveDiscount.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{discountId}", Name = "GetDiscountByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetDiscountById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetDiscountById.Response>> GetById([FromRoute]GetDiscountById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Discount == null)
            {
                return new NotFoundObjectResult(request.DiscountId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetDiscountsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetDiscounts.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDiscounts.Response>> Get()
            => await _mediator.Send(new GetDiscounts.Request());           
    }
}
