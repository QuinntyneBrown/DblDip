using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class EpicBuilder
    {
        private Epic _epic;

        public static Epic WithDefaults()
        {
            return new Epic();
        }

        public EpicBuilder()
        {
            _epic = WithDefaults();
        }

        public Epic Build()
        {
            return _epic;
        }
    }
}
