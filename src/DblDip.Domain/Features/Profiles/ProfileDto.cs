using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Profiles
{
    public class ProfileDto
    {
        public Guid ProfileId { get; set;}
        public Guid? DefaultDashboardId { get; set;}
        public Guid AccountId { get; set;}
        public Email Email { get; set;}
        public PhoneNumber PhoneNumber { get; set;}
        public Guid AvatarDigitalAssetId { get; set;}
        public string Name { get; set;}
        public string Firstname { get; set;}
        public string Lastname { get; set;}
        public string DotNetType { get; set;}
        public DateTime? Deleted { get; protected set; }
    }
}
