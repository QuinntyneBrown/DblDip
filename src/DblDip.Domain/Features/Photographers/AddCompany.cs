using DblDip.Core.Data;
using FluentValidation;
using MediatR;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class AddCompany
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid PhotographerId { get; init; }
            public CompanyDto Company { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var photographer = await _context.FindAsync<Photographer>(request.PhotographerId);

                var company = request.Company.CompanyId != default
                    ? await _context.FindAsync<Company>(request.Company.CompanyId)
                    : new Company();

                photographer.AddCompany(company.CompanyId);

                _context.Add(photographer);

                _context.Add(company);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit { };
            }
        }
    }
}
