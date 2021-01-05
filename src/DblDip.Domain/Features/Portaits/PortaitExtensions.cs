using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class PortraitExtensions
    {
        public static PortraitDto ToDto(this Portrait portrait)
        {
            return new PortraitDto(portrait.PortraitId);
        }
    }
}
