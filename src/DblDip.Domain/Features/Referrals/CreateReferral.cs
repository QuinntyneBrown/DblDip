using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateReferral
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Referral).NotNull();
                RuleFor(request => request.Referral).SetValidator(new ReferralValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ReferralDto Referral { get; init; }
        }

        public class Response
        {
            public ReferralDto Referral { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var referral = new Referral();

                _context.Add(referral);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Referral = referral.ToDto()
                };
            }
        }
    }
}
