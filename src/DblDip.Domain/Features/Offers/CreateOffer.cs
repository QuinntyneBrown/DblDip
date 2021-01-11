using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateOffer
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Offer).NotNull();
                RuleFor(request => request.Offer).SetValidator(new OfferValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public OfferDto Offer { get; init; }
        }

        public class Response
        {
            public OfferDto Offer { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var offer = new Offer();

                _context.Add(offer);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Offer = offer.ToDto()
                };
            }
        }
    }
}
