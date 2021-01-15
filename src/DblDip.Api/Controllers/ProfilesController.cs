using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Profiles;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController
    {
        private readonly IMediator _mediator;

        public ProfilesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpGet("current/account",Name = "GetProfilesByCurrentAccountRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProfilesByCurrentAccount.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProfilesByCurrentAccount.Response>> Get()
            => await _mediator.Send(new GetProfilesByCurrentAccount.Request());           
    }
}
