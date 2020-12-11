using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Expenses;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpensesController
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateExpenseRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateExpense.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateExpense.Response>> Create([FromBody]CreateExpense.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{expenseId}", Name = "RemoveExpenseRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveExpense.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{expenseId}", Name = "GetExpenseByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetExpenseById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetExpenseById.Response>> GetById([FromRoute]GetExpenseById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Expense == null)
            {
                return new NotFoundObjectResult(request.ExpenseId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetExpensesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetExpenses.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetExpenses.Response>> Get()
            => await _mediator.Send(new GetExpenses.Request());           
    }
}
