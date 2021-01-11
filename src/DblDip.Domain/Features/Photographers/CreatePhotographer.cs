using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DblDip.Core.ValueObjects;
using DblDip.Domain.IntegrationEvents;

namespace DblDip.Domain.Features
{
    public class CreatePhotographer
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
            public PhotographerDto Photographer { get; init; }
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

                var photographer = new Photographer(request.Name, request.Email);

                _context.Add(photographer);

                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new ProfileCreated(photographer));

                return new Response()
                {
                    Photographer = photographer.ToDto()
                };
            }
        }
    }
}
