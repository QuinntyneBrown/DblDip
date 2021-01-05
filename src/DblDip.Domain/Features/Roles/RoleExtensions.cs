using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class RoleExtensions
    {
        public static RoleDto ToDto(this Role role)
        {
            return new RoleDto(role.RoleId, role.Name, role.Privileges);
        }
    }
}
