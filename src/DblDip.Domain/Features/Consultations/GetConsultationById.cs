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
    public class GetConsultationById
    {
        public class Request : IRequest<Response>
        {
            public Guid ConsultationId { get; init; }
        }

        public class Response: ResponseBase
        {
            public ConsultationDto Consultation { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

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
