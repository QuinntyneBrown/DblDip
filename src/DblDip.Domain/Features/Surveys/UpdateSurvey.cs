using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            public SurveyDto Survey { get; init; }
        }

        public class Response
        {
            public SurveyDto Survey { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var survey = await _store.FindAsync<Survey>(request.Survey.SurveyId);

                survey.Update();

                _store.Add(survey);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Survey = survey.ToDto()
                };
            }
        }
    }
}
