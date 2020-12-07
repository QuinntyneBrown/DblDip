using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Consultations
{
    public class GetConsultationById
    {
        public class Request : IRequest<Response>
        {
            public Guid ConsultationId { get; set; }
        }

        public class Response
        {
            public ConsultationDto Consultation { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var consultation = await _context.FindAsync<Consultation>(request.ConsultationId);

                return new Response()
                {
                    Consultation = consultation.ToDto()
                };
            }
        }
    }
}
