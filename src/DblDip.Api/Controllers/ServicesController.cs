using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Services;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateServiceRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateService.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateService.Response>> Create([FromBody] CreateService.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateServiceRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateService.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateService.Response>> Update([FromBody] UpdateService.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{serviceId}", Name = "RemoveServiceRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveService.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{serviceId}", Name = "GetServiceByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetServiceById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetServiceById.Response>> GetById([FromRoute] GetServiceById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Service == null)
            {
                return new NotFoundObjectResult(request.ServiceId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetServicesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetServices.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetServices.Response>> Get()
            => await _mediator.Send(new GetServices.Request());
    }
}
