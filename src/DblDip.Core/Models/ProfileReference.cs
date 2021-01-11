using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class ProfileReference
    {
        protected ProfileReference()
        {

        }

        public ProfileReference(Guid profileId, string name)
        {
            ProfileId = profileId;
            Name = name;
        }
        public Guid ProfileId { get; init; }
        public string Name { get; init; }
    }
}
