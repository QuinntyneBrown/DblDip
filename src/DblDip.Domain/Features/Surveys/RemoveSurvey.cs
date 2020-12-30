using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Surveys
{
    public class RemoveSurvey
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid SurveyId { get; init; }
        }

        public class Response
        {
            public SurveyDto Survey { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var survey = await _context.FindAsync<Survey>(request.SurveyId);

                survey.Remove(_dateTime.UtcNow);

                _context.Store(survey);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
