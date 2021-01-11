using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreatePortrait
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Portrait).NotNull();
                RuleFor(request => request.Portrait).SetValidator(new PortraitValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PortraitDto Portrait { get; init; }
        }

        public class Response
        {
            public PortraitDto Portrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var portrait = new DblDip.Core.Models.Portrait();

                _context.Add(portrait);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Portrait = portrait.ToDto()
                };
            }
        }
    }
}
