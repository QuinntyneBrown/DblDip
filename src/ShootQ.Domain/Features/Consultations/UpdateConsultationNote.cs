using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Consultations
{
    public class UpdateConsultationNote
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Consultation).NotNull();
                RuleFor(request => request.Consultation).SetValidator(new ConsultationValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public ConsultationDto Consultation { get; set; }
        }

        public class Response
        {
            public ConsultationDto Consultation { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var consultation = await _context.FindAsync<Consultation>(request.Consultation.ConsultationId);

                consultation.AddNote(request.Consultation.Note);

                _context.Store(consultation);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Consultation = consultation.ToDto()
                };
            }
        }
    }
}
