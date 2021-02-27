using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public record RoleReference
    {
        public Guid RoleId { get; init; }
        public RoleReference(Guid roleId)
        {
            RoleId = roleId;
        }

        private RoleReference()
        {

        }
    }
}
