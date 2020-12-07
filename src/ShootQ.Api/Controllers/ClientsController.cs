using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Clients;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPut(Name = "UpdateClientRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateClient.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateClient.Response>> Update([FromBody]UpdateClient.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPost(Name = "CreateClientRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateClient.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateClient.Response>> Create([FromBody] CreateClient.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{clientId}", Name = "RemoveClientRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveClient.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{clientId}", Name = "GetClientByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetClientById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetClientById.Response>> GetById([FromRoute]GetClientById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Client == null)
            {
                return new NotFoundObjectResult(request.ClientId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetClientsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetClients.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetClients.Response>> Get()
            => await _mediator.Send(new GetClients.Request());           
    }
}
