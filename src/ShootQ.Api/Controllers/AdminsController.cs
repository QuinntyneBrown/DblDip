using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Admins;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminsController
    {
        private readonly IMediator _mediator;

        public AdminsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateAdminRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateAdmin.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateAdmin.Response>> Create([FromBody]CreateAdmin.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{adminId}", Name = "RemoveAdminRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveAdmin.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{adminId}", Name = "GetAdminByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAdminById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetAdminById.Response>> GetById([FromRoute]GetAdminById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Admin == null)
            {
                return new NotFoundObjectResult(request.AdminId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetAdminsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAdmins.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAdmins.Response>> Get()
            => await _mediator.Send(new GetAdmins.Request());           
    }
}
