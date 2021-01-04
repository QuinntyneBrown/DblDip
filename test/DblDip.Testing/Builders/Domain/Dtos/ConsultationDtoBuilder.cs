using DblDip.Domain.Features.Consultations;

namespace DblDip.Testing.Builders
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
