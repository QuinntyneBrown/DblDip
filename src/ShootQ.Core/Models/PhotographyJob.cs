using BuildingBlocks.Abstractions;
using ShootQ.Core.Interfaces;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public abstract class PhotographyJob : AggregateRoot, IScheduledAggregate
    {
        public Guid PhotographerId { get; set; }

        public abstract DateRange Scheduled { get; }
    }
}
