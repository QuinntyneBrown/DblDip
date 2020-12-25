using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Services
{
    public class UpdateService
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
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var service = await _context.FindAsync<Service>(request.Service.ServiceId);

                service.Update(default);

                _context.Store(service);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Service = service.ToDto()
                };
            }
        }
    }
}
