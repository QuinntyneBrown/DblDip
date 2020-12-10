using ShootQ.Core.Models;
using ShootQ.Domain.Features.Roles;

namespace ShootQ.Domain.Features
{
    public static class RoleExtensions
    {
        public static RoleDto ToDto(this Role role)
        {
            return new RoleDto(role.RoleId, role.Name, role.Privileges);
        }
    }
}
