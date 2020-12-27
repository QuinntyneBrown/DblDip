using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;

namespace DblDip.Core.Models
{
    public record WeddingPart(DateRange Scheduled, Location Location, string Description) : IScheduled;
}
