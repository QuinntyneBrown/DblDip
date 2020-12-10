using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Feedbacks;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/feedbacks")]
    public class FeedbacksController
    {
        private readonly IMediator _mediator;

        public FeedbacksController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateFeedbackRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateFeedback.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateFeedback.Response>> Create([FromBody] CreateFeedback.Request request)
            => await _mediator.Send(request);


        [Authorize]
        [HttpGet("{feedbackId}", Name = "GetFeedbackByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetFeedbackById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetFeedbackById.Response>> GetById([FromRoute] GetFeedbackById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Feedback == null)
            {
                return new NotFoundObjectResult(request.FeedbackId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetFeedbacksRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetFeedbacks.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetFeedbacks.Response>> Get()
            => await _mediator.Send(new GetFeedbacks.Request());
    }
}
