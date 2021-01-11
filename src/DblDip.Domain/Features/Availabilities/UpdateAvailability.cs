using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateAvailability
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Availability).NotNull();
                RuleFor(request => request.Availability).SetValidator(new AvailabilityValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public AvailabilityDto Availability { get; init; }
        }

        public class Response
        {
            public AvailabilityDto Availability { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var availability = await _context.FindAsync<Availability>(request.Availability.AvailabilityId);

                availability.Update();

                _context.Add(availability);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Availability = availability.ToDto()
                };
            }
        }
    }
}
