using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Engagements
{
    public class UpdateEngagement
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Engagement).NotNull();
                RuleFor(request => request.Engagement).SetValidator(new EngagementValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public EngagementDto Engagement { get; set; }
        }

        public class Response
        {
            public EngagementDto Engagement { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var engagement = await _context.FindAsync<Engagement>(request.Engagement.EngagementId);

                //engagement.Update();

                _context.Store(engagement);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Engagement = engagement.ToDto()
                };
            }
        }
    }
}