using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Search;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    public class SearchController
    {
        private readonly IMediator _mediator;
        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Search.Response>> Search([FromRoute]Search.Request request)
        {
            return await _mediator.Send(request);
        }
    }
}
