using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using FluentValidation;
using MediatR;
using DblDip.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            public Guid LeadId { get; init; }
        }

        public class Response
        {
            public LeadDto Lead { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IDblDipDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IDblDipDbContext context, IDateTime dateTime) => (_context, _dateTime) = (context, dateTime);

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var lead = await _context.FindAsync<Lead>(request.LeadId);

                lead.Remove(_dateTime.UtcNow);

                _context.Add(lead);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit { };
            }
        }
    }
}
