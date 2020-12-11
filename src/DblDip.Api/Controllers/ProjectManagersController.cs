using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.ProjectManagers;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/projectManagers")]
    public class ProjectManagersController
    {
        private readonly IMediator _mediator;

        public ProjectManagersController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateProjectManagerRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateProjectManager.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateProjectManager.Response>> Create([FromBody]CreateProjectManager.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{projectManagerId}", Name = "RemoveProjectManagerRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveProjectManager.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{projectManagerId}", Name = "GetProjectManagerByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProjectManagerById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetProjectManagerById.Response>> GetById([FromRoute]GetProjectManagerById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.ProjectManager == null)
            {
                return new NotFoundObjectResult(request.ProjectManagerId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetProjectManagersRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProjectManagers.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProjectManagers.Response>> Get()
            => await _mediator.Send(new GetProjectManagers.Request());           
    }
}
