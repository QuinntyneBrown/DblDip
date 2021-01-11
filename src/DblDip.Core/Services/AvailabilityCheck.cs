using BuildingBlocks.EventStore;
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


        public Task<bool> IsAvailable(DateRange dateRange)
        {
            return System.Threading.Tasks.Task.FromResult(true);
        }
    }
}
