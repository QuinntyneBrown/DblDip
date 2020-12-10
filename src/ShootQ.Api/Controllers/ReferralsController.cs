using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Referrals;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/referrals")]
    public class ReferralsController
    {
        private readonly IMediator _mediator;

        public ReferralsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateReferralRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateReferral.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateReferral.Response>> Create([FromBody] CreateReferral.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{referralId}", Name = "RemoveReferralRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveReferral.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{referralId}", Name = "GetReferralByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetReferralById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetReferralById.Response>> GetById([FromRoute] GetReferralById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Referral == null)
            {
                return new NotFoundObjectResult(request.ReferralId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetReferralsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetReferrals.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetReferrals.Response>> Get()
            => await _mediator.Send(new GetReferrals.Request());
    }
}
