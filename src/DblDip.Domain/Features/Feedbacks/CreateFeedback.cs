using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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

        public class Request : IRequest<Response>
        {
            public FeedbackDto Feedback { get; init; }
        }

        public class Response
        {
            public FeedbackDto Feedback { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var feedback = new Feedback(request.Feedback.RespondentEmail, request.Feedback.Description);

                _context.Add(feedback);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Feedback = feedback.ToDto()
                };
            }
        }
    }
}
