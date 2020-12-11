using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Leads
{
    public class CreateLead
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Lead).NotNull();
                RuleFor(request => request.Lead).SetValidator(new LeadValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public LeadDto Lead { get; set; }
        }

        public class Response
        {
            public LeadDto Lead { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var lead = new Lead();

                _context.Store(lead);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Lead = lead.ToDto()
                };
            }
        }
    }
}
