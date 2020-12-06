using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.SystemLocations
{
    public class UpdateSystemLocation
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.SystemLocation).NotNull();
                RuleFor(request => request.SystemLocation).SetValidator(new SystemLocationValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public SystemLocationDto SystemLocation { get; set; }
        }

        public class Response
        {
            public SystemLocationDto SystemLocation { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var systemLocation = await _context.FindAsync<SystemLocation>(request.SystemLocation.SystemLocationId);



                _context.Store(systemLocation);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    SystemLocation = systemLocation.ToDto()
                };
            }
        }
    }
}
