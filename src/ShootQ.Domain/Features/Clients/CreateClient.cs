using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Clients
{
    public class CreateClient
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Client).NotNull();
                RuleFor(request => request.Client).SetValidator(new ClientValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public ClientDto Client { get; set; }
        }

        public class Response
        {
            public ClientDto Client { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var client = new Client();

                _context.Store(client);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Client = client.ToDto()
                };
            }
        }
    }
}
