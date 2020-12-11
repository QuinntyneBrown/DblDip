using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Companies;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateCompanyRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateCompany.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateCompany.Response>> Create([FromBody]CreateCompany.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{companyId}", Name = "RemoveCompanyRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveCompany.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{companyId}", Name = "GetCompanyByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCompanyById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetCompanyById.Response>> GetById([FromRoute]GetCompanyById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Company == null)
            {
                return new NotFoundObjectResult(request.CompanyId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetCompaniesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCompanies.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCompanies.Response>> Get()
            => await _mediator.Send(new GetCompanies.Request());           
    }
}
