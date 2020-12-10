using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Surveys
{
    public class UpdateSurvey
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Survey).NotNull();
                RuleFor(request => request.Survey).SetValidator(new SurveyValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public SurveyDto Survey { get; set; }
        }

        public class Response
        {
            public SurveyDto Survey { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var survey = await _context.FindAsync<Survey>(request.Survey.SurveyId);

                //survey.Update();

                _context.Store(survey);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Survey = survey.ToDto()
                };
            }
        }
    }
}
