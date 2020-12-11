using ShootQ.Core.Models;
using ShootQ.Domain.Features.Admins;

namespace ShootQ.Domain.Features
{
    public static class AdminExtensions
    {
        public static AdminDto ToDto(this Admin admin)
            => new AdminDto(admin.AdminId, admin.Name, admin.Email);
    }
}
