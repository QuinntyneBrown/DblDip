using DblDip.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace DblDip.Core.Models
{
    [Owned]
    public class Privilege
    {
        public AccessRight AccessRight { get; set; }
        public string Aggregate { get; set; }

        public Privilege(AccessRight accessRight, string aggregate)
        {
            AccessRight = accessRight;
            Aggregate = aggregate;
        }

        public Privilege()
        {

        }
    };
}
