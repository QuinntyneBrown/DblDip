using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Brands;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateBrandRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateBrand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateBrand.Response>> Create([FromBody]CreateBrand.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{brandId}", Name = "RemoveBrandRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveBrand.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{brandId}", Name = "GetBrandByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBrandById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetBrandById.Response>> GetById([FromRoute]GetBrandById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Brand == null)
            {
                return new NotFoundObjectResult(request.BrandId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetBrandsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBrands.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBrands.Response>> Get()
            => await _mediator.Send(new GetBrands.Request());           
    }
}
