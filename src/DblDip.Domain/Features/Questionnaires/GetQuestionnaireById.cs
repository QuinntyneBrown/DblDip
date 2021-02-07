using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetQuestionnaireById
    {
        public class Request : IRequest<Response>
        {
            public Guid QuestionnaireId { get; init; }
        }

        public class Response: ResponseBase
        {
            public QuestionnaireDto Questionnaire { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var questionnaire = await _context.FindAsync<Questionnaire>(request.QuestionnaireId);

                return new Response()
                {
                    Questionnaire = questionnaire.ToDto()
                };
            }
        }
    }
}
