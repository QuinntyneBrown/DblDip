using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.FamilyPortraits;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/familyPortraits")]
    public class FamilyPortraitsController
    {
        private readonly IMediator _mediator;

        public FamilyPortraitsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateFamilyPortraitRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateFamilyPortrait.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateFamilyPortrait.Response>> Create([FromBody] CreateFamilyPortrait.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{familyPortraitId}", Name = "RemoveFamilyPortraitRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveFamilyPortrait.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{familyPortraitId}", Name = "GetFamilyPortraitByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetFamilyPortraitById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetFamilyPortraitById.Response>> GetById([FromRoute] GetFamilyPortraitById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.FamilyPortrait == null)
            {
                return new NotFoundObjectResult(request.FamilyPortraitId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetFamilyPortraitsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetFamilyPortraits.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetFamilyPortraits.Response>> Get()
            => await _mediator.Send(new GetFamilyPortraits.Request());
    }
}
