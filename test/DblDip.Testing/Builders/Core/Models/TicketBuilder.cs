using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class TicketBuilder
    {
        private Ticket _ticket;

        public static Ticket WithDefaults()
        {
            return new Ticket();
        }

        public TicketBuilder()
        {
            _ticket = WithDefaults();
        }

        public Ticket Build()
        {
            return _ticket;
        }
    }
}
