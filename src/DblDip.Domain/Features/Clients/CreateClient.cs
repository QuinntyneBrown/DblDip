using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DblDip.Core.ValueObjects;
using DblDip.Domain.IntegrationEvents;
using DblDip.Core.Data;

namespace DblDip.Domain.Features
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
            public string Name { get; set; }
            public Email Email { get; set; }
        }

        public class Response
        {
            public ClientDto Client { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IDblDipDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }


            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var client = new Client(request.Name, request.Email);

                _context.Add(client);

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
