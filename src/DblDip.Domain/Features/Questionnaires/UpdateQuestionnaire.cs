using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateQuestionnaire
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Questionnaire).NotNull();
                RuleFor(request => request.Questionnaire).SetValidator(new QuestionnaireValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public QuestionnaireDto Questionnaire { get; init; }
        }

        public class Response: ResponseBase
        {
            public QuestionnaireDto Questionnaire { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var questionnaire = await _store.FindAsync<Questionnaire>(request.Questionnaire.QuestionnaireId);

                questionnaire.Update();

                _store.Add(questionnaire);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Questionnaire = questionnaire.ToDto()
                };
            }
        }
    }
}
