using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DblDip.Core.Models
{
    [Owned]
    public class WeddingPart : IScheduled
    {
        public DateRange Scheduled { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public WeddingPart(DateRange scheduled, Location location, string description)
        {
            Scheduled = scheduled;
            Location = location;
            Description = description;
        }
        public WeddingPart()
        {

        }
    }
}
