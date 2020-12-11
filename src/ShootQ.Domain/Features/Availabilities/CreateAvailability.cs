using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Availabilities
{
    public class CreateAvailability
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Availability).NotNull();
                RuleFor(request => request.Availability).SetValidator(new AvailabilityValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public AvailabilityDto Availability { get; set; }
        }

        public class Response
        {
            public AvailabilityDto Availability { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var availability = new Availability();

                _context.Store(availability);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Availability = availability.ToDto()
                };
            }
        }
    }
}
