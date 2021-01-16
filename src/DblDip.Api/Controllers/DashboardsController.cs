using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/dashboards")]
    public class DashboardsController
    {
        private readonly IMediator _mediator;

        public DashboardsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateDashboardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateDashboard.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateDashboard.Response>> Create([FromBody] CreateDashboard.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateDashboardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateDashboard.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateDashboard.Response>> Update([FromBody] UpdateDashboard.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("current-profile", Name = "GetDashboardsByCurrentProfileRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDashboardsByCurrentProfile.Response>> Get()
            => await _mediator.Send(new GetDashboardsByCurrentProfile.Request());

        [Authorize]
        [HttpGet("default", Name = "GetDefaultDashboardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDefaultDashboard.Response>> GetDefault()
            => await _mediator.Send(new GetDefaultDashboard.Request());

        [Authorize]
        [HttpDelete("{dashboardId}", Name = "RemoveDashboardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveDashboard.Request request)
            => await _mediator.Send(request);
    }
}
