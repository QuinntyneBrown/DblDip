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

        public ProfileReference(Guid profileId)
        {
            ProfileId = profileId;
        }
        public Guid ProfileId { get; init; }
    }
}
