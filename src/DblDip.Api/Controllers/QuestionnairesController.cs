using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/questionnaires")]
    public class QuestionnairesController
    {
        private readonly IMediator _mediator;

        public QuestionnairesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateQuestionnaireRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateQuestionnaire.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateQuestionnaire.Response>> Create([FromBody] CreateQuestionnaire.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{questionnaireId}", Name = "RemoveQuestionnaireRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveQuestionnaire.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{questionnaireId}", Name = "GetQuestionnaireByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetQuestionnaireById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetQuestionnaireById.Response>> GetById([FromRoute] GetQuestionnaireById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Questionnaire == null)
            {
                return new NotFoundObjectResult(request.QuestionnaireId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetQuestionnairesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetQuestionnaires.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetQuestionnaires.Response>> Get()
            => await _mediator.Send(new GetQuestionnaires.Request());
    }
}
