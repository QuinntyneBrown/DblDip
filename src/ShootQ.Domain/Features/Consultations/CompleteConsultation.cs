using BuildingBlocks.Abstractions;
using MediatR;
using ShootQ.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Consultations
{
    public class CompleteConsultation
    {
        public record Request(Guid ConsultationId) : IRequest<Unit>;

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

                var consultation = await _context.FindAsync<Consultation>(request.ConsultationId);

                consultation.Complete(_dateTime.UtcNow);

                _context.Store(consultation);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
