using DblDip.Core.Models;

namespace DblDip.Domain.Features
{
    public static class ProfileExtensions
    {
        public static ProfileDto ToDto(this Profile profile)
        {
            return new ProfileDto
            {
                ProfileId = profile.ProfileId,
                DefaultDashboardId = profile.DefaultDashboardId,
                AccountId = profile.AccountId,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                AvatarDigitalAssetId = profile.AvatarDigitalAssetId,
                Name = profile.Name,
                Firstname = profile.Firstname,
                Lastname = profile.Lastname,
                Type = profile.Type,
                DotNetType = profile.DotNetType
            };
        }
    }
}
