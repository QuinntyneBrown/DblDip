using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Domain.Features.Consultations
{
    public class CreateConsultation
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

                var dateRange = DateRange.Create(request.Consultation.StartDate, request.Consultation.EndDate).Value;

                var consultation = new Consultation(dateRange,request.Consultation.CustomerEmail);

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
