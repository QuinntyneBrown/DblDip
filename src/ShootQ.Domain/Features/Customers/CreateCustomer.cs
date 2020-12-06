using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ShootQ.Domain.Features.Customers
{
    public class CreateCustomer
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Customer).NotNull();
                RuleFor(request => request.Customer).SetValidator(new CustomerValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public CustomerDto Customer { get; set; }
        }

        public class Response
        {
            public CustomerDto Customer { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;

            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var customer = new Customer();

                _context.Store(customer);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Customer = customer.ToDto()
                };
            }
        }
    }
}
