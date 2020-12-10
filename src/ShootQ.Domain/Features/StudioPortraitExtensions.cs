using ShootQ.Core.Models;
using ShootQ.Domain.Features.StudioPortraits;

namespace ShootQ.Domain.Features
{
    public static class StudioPortraitExtensions
    {
        public static StudioPortraitDto ToDto(this StudioPortrait studioPortrait)
        {
            return new StudioPortraitDto(studioPortrait.StudioPortraitId);
        }
    }
}
