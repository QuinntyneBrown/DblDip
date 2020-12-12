using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Companies
{
    public class CreateCompany
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Company).NotNull();
                RuleFor(request => request.Company).SetValidator(new CompanyValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public CompanyDto Company { get; set; }
        }

        public class Response
        {
            public CompanyDto Company { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var company = new Company(default);

                _context.Store(company);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Company = company.ToDto()
                };
            }
        }
    }
}
