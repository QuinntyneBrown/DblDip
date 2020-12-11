using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ShootQ.Domain.Features.Availabilities
{
    public class RemoveAvailability
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid AvailabilityId { get; set; }
        }

        public class Response
        {
            public AvailabilityDto Availability { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var availability = await _context.FindAsync<Availability>(request.AvailabilityId);

                //availability.Remove();

                _context.Store(availability);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
