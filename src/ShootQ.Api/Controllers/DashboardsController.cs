using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Dashboards;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
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
        public async Task<ActionResult<CreateDashboard.Response>> Create([FromBody]CreateDashboard.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{dashboardId}", Name = "RemoveDashboardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveDashboard.Request request)
            => await _mediator.Send(request);

       
    }
}
