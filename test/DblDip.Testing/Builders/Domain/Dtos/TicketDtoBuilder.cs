using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class TicketDtoBuilder
    {
        private TicketDto _ticketDto;

        public static TicketDto WithDefaults()
        {
            return new TicketDto();
        }

        public TicketDtoBuilder()
        {
            _ticketDto = WithDefaults();
        }

        public TicketDto Build()
        {
            return _ticketDto;
        }
    }
}
