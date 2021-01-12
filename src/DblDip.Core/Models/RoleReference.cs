using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class RoleReference
    {
        public Guid RoleId { get; set; }
        public RoleReference(Guid roleId)
        {
            RoleId = roleId;
        }

        public RoleReference()
        {

        }
    }
}
