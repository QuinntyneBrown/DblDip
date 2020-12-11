using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Questionnaires
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

        public class Request : IRequest<Response> {  
            public QuestionnaireDto Questionnaire { get; set; }
        }

        public class Response
        {
            public QuestionnaireDto Questionnaire { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var questionnaire = await _context.FindAsync<Questionnaire>(request.Questionnaire.QuestionnaireId);

                //questionnaire.Update();

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
