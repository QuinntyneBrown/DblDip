using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DblDip.Domain.Features.Testimonials;
using System.Net;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/testimonials")]
    public class TestimonialsController
    {
        private readonly IMediator _mediator;

        public TestimonialsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateTestimonialRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateTestimonial.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateTestimonial.Response>> Create([FromBody] CreateTestimonial.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut(Name = "UpdateTestimonialRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateTestimonial.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateTestimonial.Response>> Update([FromBody] UpdateTestimonial.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{testimonialId}", Name = "RemoveTestimonialRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveTestimonial.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{testimonialId}", Name = "GetTestimonialByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTestimonialById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetTestimonialById.Response>> GetById([FromRoute] GetTestimonialById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Testimonial == null)
            {
                return new NotFoundObjectResult(request.TestimonialId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetTestimonialsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTestimonials.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTestimonials.Response>> Get()
            => await _mediator.Send(new GetTestimonials.Request());
    }
}
