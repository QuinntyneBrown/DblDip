namespace DblDip.Core.Models
{
    public class Shot {

        public Shot(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; init; }

        public string Description { get; init; }
    }
}
