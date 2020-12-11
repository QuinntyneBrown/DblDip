using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class SurveyBuilder
    {
        private Survey _survey;

        public static Survey WithDefaults()
        {
            return new Survey("Test");
        }

        public SurveyBuilder()
        {
            _survey = WithDefaults();
        }

        public Survey Build()
        {
            return _survey;
        }
    }
}
