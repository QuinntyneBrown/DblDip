using ShootQ.Core.Models;
using ShootQ.Domain.Features.Companies;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
