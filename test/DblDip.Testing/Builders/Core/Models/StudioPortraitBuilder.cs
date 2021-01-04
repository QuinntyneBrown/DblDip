using DblDip.Core.Models;

namespace DblDip.Testing.Builders
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
