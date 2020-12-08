using BuildingBlocks.Abstractions;
using FluentValidation;
using MediatR;
using ShootQ.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Leads
{
    public class RemoveLead
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid LeadId { get; set; }
        }

        public class Response
        {
            public LeadDto Lead { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime) => (_context, _dateTime) = (context,dateTime);

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var lead = await _context.FindAsync<Lead>(request.LeadId);

                lead.Remove(_dateTime.UtcNow);

                _context.Store(lead);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit { };
            }
        }
    }
}
