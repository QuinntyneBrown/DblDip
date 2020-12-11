using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using System;
using System.Threading.Tasks;

namespace DblDip.Core.Services
{
    public interface IAvailabilityCheck
    {
        Task<bool> IsAvailable(DateRange dateRange);
    }

    public class AvailabilityCheck : IAvailabilityCheck
    {
        private readonly IAppDbContext _context;

        public AvailabilityCheck(IAppDbContext context)
        {
            _context = context;
        }

        public Task<bool> IsAvailable(DateRange dateRange)
        {
            var orders = _context.Set<Order>();

            throw new NotImplementedException();
        }
    }
}
