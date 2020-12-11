using DblDip.Core.Models;
using DblDip.Domain.Features.Companies;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class CompanyDtoBuilder
    {
        private CompanyDto _companyDto;

        public static CompanyDto WithDefaults()
        {
            return new CompanyDto();
        }

        public CompanyDtoBuilder()
        {
            _companyDto = WithDefaults();
        }

        public CompanyDto Build()
        {
            return _companyDto;
        }
    }
}
