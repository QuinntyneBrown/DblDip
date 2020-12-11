using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class CompanyBuilder
    {
        private Company _company;

        public static Company WithDefaults()
        {
            return new Company(default);
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
