using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features
{
    public class RemoveDiscount
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid DiscountId { get; set; }
        }

        public class Response
        {
            public DiscountDto Discount { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IDblDipDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IDblDipDbContext context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var discount = await _context.FindAsync<Discount>(request.DiscountId);

                discount.Remove(_dateTime.UtcNow);

                _context.Add(discount);

                await _context.SaveChangesAsync(cancellationToken);

                return new()
                {

                };
            }
        }
    }
}
