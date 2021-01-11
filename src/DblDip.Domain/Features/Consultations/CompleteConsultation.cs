using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using MediatR;
using DblDip.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CompleteConsultation
    {
        public record Request(Guid ConsultationId) : IRequest<Unit>;

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IDblDipDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IDblDipDbContext context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var consultation = await _context.FindAsync<Consultation>(request.ConsultationId);

                consultation.Complete(_dateTime.UtcNow);

                _context.Add(consultation);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
