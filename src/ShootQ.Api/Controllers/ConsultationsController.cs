using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Consultations;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/consultations")]
    public class ConsultationsController
    {
        private readonly IMediator _mediator;

        public ConsultationsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateConsultationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateConsultation.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateConsultation.Response>> Create([FromBody] CreateConsultation.Request request)
            => await _mediator.Send(request);


        [Authorize]
        [HttpPut(Name = "UpdateConsultationNoteRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateConsultationNote.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateConsultationNote.Response>> Addote([FromBody] UpdateConsultationNote.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut("reschedule", Name = "RescheduleConsultationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RescheduleConsultation.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RescheduleConsultation.Response>> RescheduleConsultation([FromBody] RescheduleConsultation.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut("complete", Name = "CompleteConsultationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> CompleteConsultation([FromBody] CompleteConsultation.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{consultationId}", Name = "RemoveConsultationRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveConsultation.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{consultationId}", Name = "GetConsultationByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetConsultationById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetConsultationById.Response>> GetById([FromRoute] GetConsultationById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Consultation == null)
            {
                return new NotFoundObjectResult(request.ConsultationId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetConsultationsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetConsultations.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetConsultations.Response>> Get()
            => await _mediator.Send(new GetConsultations.Request());
    }
}
