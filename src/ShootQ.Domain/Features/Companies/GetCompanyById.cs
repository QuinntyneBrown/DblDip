using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Companies
{
    public class GetCompanyById
    {
        public class Request : IRequest<Response> {  
            public Guid CompanyId { get; set; }        
        }

        public class Response
        {
            public CompanyDto Company { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var company = await _context.FindAsync<Company>(request.CompanyId);

                return new Response() { 
                    Company = company.ToDto()
                };
            }
        }
    }
}
