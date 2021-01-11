using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateService
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Service).NotNull();
                RuleFor(request => request.Service).SetValidator(new ServiceValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ServiceDto Service { get; init; }
        }

        public class Response
        {
            public ServiceDto Service { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var service = new Service(default, default, default);

                _context.Add(service);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Service = service.ToDto()
                };
            }
        }
    }
}
