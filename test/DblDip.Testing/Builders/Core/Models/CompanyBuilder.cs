using DblDip.Core.Models;

namespace DblDip.Testing.Builders
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
