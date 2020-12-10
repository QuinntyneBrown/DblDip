using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Photographers
{
    public class CreatePhotographer
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Photographer).NotNull();
                RuleFor(request => request.Photographer).SetValidator(new PhotographerValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public PhotographerDto Photographer { get; set; }
        }

        public class Response
        {
            public PhotographerDto Photographer { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var photographer = new Photographer();

                _context.Store(photographer);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Photographer = photographer.ToDto()
                };
            }
        }
    }
}
