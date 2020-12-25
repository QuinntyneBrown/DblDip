using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DblDip.Core.ValueObjects;
using DblDip.Domain.IntegrationEvents;

namespace DblDip.Domain.Features.Clients
{
    public class CreateClient
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response>
        {
            public string Name { get; init; }
            public Email Email { get; init; }
        }

        public class Response
        {
            public ClientDto Client { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }


            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var client = new Client(request.Name, request.Email);

                _context.Store(client);

                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new ProfileCreated(client));

                return new Response()
                {
                    Client = client.ToDto()
                };
            }
        }
    }
}
