using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateQuestionnaire
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

        public class Response
        {
            public QuestionnaireDto Questionnaire { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var questionnaire = new Questionnaire();

                _context.Store(questionnaire);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Questionnaire = questionnaire.ToDto()
                };
            }
        }
    }
}
