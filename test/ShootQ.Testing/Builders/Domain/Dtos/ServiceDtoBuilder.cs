using ShootQ.Domain.Features.Services;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
