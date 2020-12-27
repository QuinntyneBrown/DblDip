using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;

namespace DblDip.Core.Models
{
    public record Trip(DateRange Scheduled, Location Start, Location End): IScheduled;
}
