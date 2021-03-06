using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class ServiceDtoBuilder
    {
        private ServiceDto _service;

        public static ServiceDto WithDefaults()
        {
            return new ServiceDto();
        }

        public ServiceDtoBuilder()
        {
            _service = WithDefaults();
        }

        public ServiceDto Build()
        {
            return _service;
        }
    }
}
