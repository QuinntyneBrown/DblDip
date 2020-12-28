using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class CompanyBuilder
    {
        private Company _company;

        public static Company WithDefaults()
        {
            return new Company();
        }

        public CompanyBuilder()
        {
            _company = WithDefaults();
        }

        public Company Build()
        {
            return _company;
        }
    }
}
