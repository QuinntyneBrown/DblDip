using ShootQ.Core.Models;
using ShootQ.Domain.Features.SystemLocations;

namespace ShootQ.Domain.Features
{
    public static class SystemLocationExtensions
    {
        public static SystemLocationDto ToDto(this SystemLocation systemLocation)
        {
            return new SystemLocationDto
            {
                SystemLocationId = systemLocation.SystemLocationId,
                Name = systemLocation.Name,
                Location = systemLocation.Location
            };
        }
    }
}
