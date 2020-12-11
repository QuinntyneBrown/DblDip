using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.SystemAdministrators;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/systemAdministrators")]
    public class SystemAdministratorsController
    {
        private readonly IMediator _mediator;

        public SystemAdministratorsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateSystemAdministratorRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateSystemAdministrator.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateSystemAdministrator.Response>> Create([FromBody]CreateSystemAdministrator.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{systemAdministratorId}", Name = "RemoveSystemAdministratorRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveSystemAdministrator.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{systemAdministratorId}", Name = "GetSystemAdministratorByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSystemAdministratorById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetSystemAdministratorById.Response>> GetById([FromRoute]GetSystemAdministratorById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.SystemAdministrator == null)
            {
                return new NotFoundObjectResult(request.SystemAdministratorId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetSystemAdministratorsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSystemAdministrators.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSystemAdministrators.Response>> Get()
            => await _mediator.Send(new GetSystemAdministrators.Request());           
    }
}
