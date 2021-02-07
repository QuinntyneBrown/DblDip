using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetSurveyById
    {
        public class Request : IRequest<Response>
        {
            public Guid SurveyId { get; init; }
        }

        public class Response: ResponseBase
        {
            public SurveyDto Survey { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var survey = await _context.FindAsync<Survey>(request.SurveyId);

                return new Response()
                {
                    Survey = survey.ToDto()
                };
            }
        }
    }
}
