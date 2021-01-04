using MediatR;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController
    {
        private readonly IMediator _mediator;
        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Search.Response>> Search([FromRoute] Search.Request request)
        {
            return await _mediator.Send(request);
        }
    }
}
