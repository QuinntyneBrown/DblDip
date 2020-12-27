using System;

namespace DblDip.Core.Models
{
    public class ProfileReference
    {
        public ProfileReference(Guid profileId, string name)
        {
            ProfileId = profileId;
            Name = name;
        }
        public Guid ProfileId { get; init; }
        public string Name { get; init; }
    }
}
