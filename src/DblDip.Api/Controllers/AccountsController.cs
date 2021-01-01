using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Accounts;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateAccountRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateAccount.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateAccount.Response>> Create([FromBody]CreateAccount.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "SetCurrentProfileRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SetCurrentProfile.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SetCurrentProfile.Response>> Update([FromBody] SetCurrentProfile.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateAccountRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateAccount.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateAccount.Response>> Update([FromBody]UpdateAccount.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{accountId}", Name = "RemoveAccountRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveAccount.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{accountId}", Name = "GetAccountByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAccountById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetAccountById.Response>> GetById([FromRoute]GetAccountById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Account == null)
            {
                return new NotFoundObjectResult(request.AccountId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetAccountsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAccounts.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAccounts.Response>> Get()
            => await _mediator.Send(new GetAccounts.Request());           
    }
}
