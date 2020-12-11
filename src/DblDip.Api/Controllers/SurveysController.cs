using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Surveys;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/surveys")]
    public class SurveysController
    {
        private readonly IMediator _mediator;

        public SurveysController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateSurveyRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateSurvey.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateSurvey.Response>> Create([FromBody] CreateSurvey.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateSurveyRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateSurvey.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateSurvey.Response>> Update([FromBody] UpdateSurvey.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{surveyId}", Name = "RemoveSurveyRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveSurvey.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{surveyId}", Name = "GetSurveyByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSurveyById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetSurveyById.Response>> GetById([FromRoute] GetSurveyById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Survey == null)
            {
                return new NotFoundObjectResult(request.SurveyId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetSurveysRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSurveys.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSurveys.Response>> Get()
            => await _mediator.Send(new GetSurveys.Request());
    }
}
