using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateVenue
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Venue).NotNull();
                RuleFor(request => request.Venue).SetValidator(new VenueValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public VenueDto Venue { get; init; }
        }

        public class Response
        {
            public VenueDto Venue { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var venue = new Venue();

                _context.Add(venue);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Venue = venue.ToDto()
                };
            }
        }
    }
}
