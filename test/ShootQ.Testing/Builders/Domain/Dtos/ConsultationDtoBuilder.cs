using ShootQ.Domain.Features.Consultations;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class ConsultationDtoBuilder
    {
        private ConsultationDto _consultationDto;

        public static ConsultationDto WithDefaults()
        {
            return new ConsultationDto(default, default, default, default, default, default);
        }

        public ConsultationDtoBuilder()
        {
            _consultationDto = WithDefaults();
        }

        public ConsultationDto Build()
        {
            return _consultationDto;
        }
    }
}
