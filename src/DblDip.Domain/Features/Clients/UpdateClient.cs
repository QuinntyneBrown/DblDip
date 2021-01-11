using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateClient
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Client).NotNull();
                RuleFor(request => request.Client).SetValidator(new ClientValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ClientDto Client { get; init; }
        }

        public class Response
        {
            public ClientDto Client { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var client = await _context.FindAsync<Client>(request.Client.ClientId);

                client.Update();

                _context.Add(client);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Client = client.ToDto()
                };
            }
        }
    }
}
