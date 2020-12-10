using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Equipment;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/equipment")]
    public class EquipmentController
    {
        private readonly IMediator _mediator;

        public EquipmentController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateEquipmentRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateEquipment.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateEquipment.Response>> Create([FromBody]CreateEquipment.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{equipmentId}", Name = "RemoveEquipmentRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveEquipment.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{equipmentId}", Name = "GetEquipmentByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetEquipmentById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetEquipmentById.Response>> GetById([FromRoute]GetEquipmentById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Equipment == null)
            {
                return new NotFoundObjectResult(request.EquipmentId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetEquipmentRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetEquipment.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetEquipment.Response>> Get()
            => await _mediator.Send(new GetEquipment.Request());           
    }
}
