using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Tasks;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateTaskRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateTask.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateTask.Response>> Create([FromBody] CreateTask.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{taskId}", Name = "RemoveTaskRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveTask.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{taskId}", Name = "GetTaskByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTaskById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetTaskById.Response>> GetById([FromRoute] GetTaskById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Task == null)
            {
                return new NotFoundObjectResult(request.TaskId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetTasksRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTasks.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTasks.Response>> Get()
            => await _mediator.Send(new GetTasks.Request());
    }
}
