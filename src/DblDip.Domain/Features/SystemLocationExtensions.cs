using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
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
