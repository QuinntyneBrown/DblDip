using DblDip.Core.Data;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            public LeadDto Lead { get; init; }
        }

        public class Response
        {
            public LeadDto Lead { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var lead = new Lead((Email)request.Lead.EmailAddress);

                _context.Add(lead);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Lead = lead.ToDto()
                };
            }
        }
    }
}
