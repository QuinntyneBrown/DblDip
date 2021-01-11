using Microsoft.EntityFrameworkCore;

namespace DblDip.Core.Models
{
    [Owned]
    public class Shot
    {
        public string Name { get; init; }
        public string Description { get; init; }
        protected Shot()
        {

        }

        public Shot(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
