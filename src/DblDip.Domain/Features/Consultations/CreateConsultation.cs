using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DblDip.Core.ValueObjects;

namespace DblDip.Domain.Features
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

        public class Request : IRequest<Response>
        {
            public ConsultationDto Consultation { get; init; }
        }

        public class Response
        {
            public ConsultationDto Consultation { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var dateRange = DateRange.Create(request.Consultation.StartDate, request.Consultation.EndDate).Value;

                var consultation = new Consultation(dateRange, request.Consultation.ConsultantEmail, request.Consultation.RecipientEmail);

                _store.Add(consultation);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Consultation = consultation.ToDto()
                };
            }
        }
    }
}
