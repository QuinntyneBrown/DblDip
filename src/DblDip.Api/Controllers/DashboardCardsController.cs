using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/dashboard/{dashboardId}/cards")]
    public class DashboardCardsController
    {
        private readonly IMediator _mediator;

        public DashboardCardsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "UpdateDashboardsCardRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateDashboardCards.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateDashboardCards.Response>> Create([FromBody] UpdateDashboardCards.Request request, [FromRoute] Guid dashboardId)
            => await _mediator.Send(request with { DashboardId = dashboardId });
    }
}
