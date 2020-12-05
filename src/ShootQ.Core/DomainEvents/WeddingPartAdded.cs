using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record WeddingPartAdded(DateTime DateTime, int Hours, Location Location, string Description);
}
