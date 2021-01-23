using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class ProfileReference
    {
        public Guid ProfileId { get; init; }
        protected ProfileReference()
        {

        }

        public ProfileReference(Guid profileId)
        {
            ProfileId = profileId;
        }

    }
}
