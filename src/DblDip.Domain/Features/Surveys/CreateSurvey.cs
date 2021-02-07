using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateSurvey
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

        public class Response: ResponseBase
        {
            public SurveyDto Survey { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var survey = new Survey(request.Survey.Name);

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
