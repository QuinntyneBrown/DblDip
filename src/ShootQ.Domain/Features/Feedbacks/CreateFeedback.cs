using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Feedbacks
{
    public class CreateFeedback
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Feedback).NotNull();
                RuleFor(request => request.Feedback).SetValidator(new FeedbackValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public FeedbackDto Feedback { get; set; }
        }

        public class Response
        {
            public FeedbackDto Feedback { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var feedback = new Feedback(request.Feedback.ClientEmail,request.Feedback.Description);

                _context.Store(feedback);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Feedback = feedback.ToDto()
                };
            }
        }
    }
}
