using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DblDip.Core.Models
{
    [Owned]
    public class Trip: IScheduled
    {
        public DateRange Scheduled { get; set; }
        public Location Start { get; set; }
        public Location End { get; set; }
        public Trip(DateRange scheduled, Location start, Location end)
        {
            Scheduled = scheduled;
            Start = start;
            End = end;
        }
        public Trip()
        {

        }
    }
}
