using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class StudioPortraitBuilder
    {
        private StudioPortrait _studioPortrait;

        public static StudioPortrait WithDefaults()
        {
            return new StudioPortrait();
        }

        public StudioPortraitBuilder()
        {
            _studioPortrait = WithDefaults();
        }

        public StudioPortrait Build()
        {
            return _studioPortrait;
        }
    }
}
