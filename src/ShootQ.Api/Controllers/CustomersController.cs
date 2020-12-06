using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Customers;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateCustomerRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateCustomer.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateCustomer.Response>> Create([FromBody]CreateCustomer.Request request)
        {
            return await _mediator.Send(request);
        }

        [Authorize]
        [HttpDelete("{customerId}", Name = "RemoveCustomerRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveCustomer.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{customerId}", Name = "GetCustomerByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCustomerById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetCustomerById.Response>> GetById([FromRoute]GetCustomerById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Customer == null)
            {
                return new NotFoundObjectResult(request.CustomerId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetCustomersRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCustomers.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCustomers.Response>> Get()
            => await _mediator.Send(new GetCustomers.Request());           
    }
}
