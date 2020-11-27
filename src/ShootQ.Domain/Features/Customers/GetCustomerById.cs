using BuildingBlocks.Abstractions;
using MediatR;
using ShootQ.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Customers
{
    public class GetCustomerById
    {
        public class Request : IRequest<Response>
        {
            public Guid CustomerId { get; set; }
        }

        public class Response
        {
            public CustomerDto Customer { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var customer = await _context.FindAsync<Customer>(request.CustomerId);

                return new Response()
                {
                    Customer = customer.ToDto()
                };
            }
        }
    }
}
