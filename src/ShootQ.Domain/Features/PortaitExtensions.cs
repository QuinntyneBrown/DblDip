using ShootQ.Core.Models;
using ShootQ.Domain.Features.Portraits;

namespace ShootQ.Domain.Features
{
    public static class PortraitExtensions
    {
        public static PortraitDto ToDto(this Portrait portrait)
        {
            return new PortraitDto(portrait.PortraitId);
        }
    }
}
