using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class RoleReference
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public RoleReference(Guid roleId, string name)
        {
            RoleId = roleId;
            Name = name;
        }

        public RoleReference()
        {

        }
    }
}
